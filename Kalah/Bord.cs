using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalah
{

    

    class Board
    {
        const int N = 7;
        public int[,] board = 
            {   {  0, 6, 6, 6, 6, 6, 6, -1 },
                { -1, 6, 6, 6, 6, 6, 6,  0 }   };

        //  0 6 6 6 6 6 6 -1
        // -1 6 6 6 6 6 6  0

        public Board(int n)  { INIT(); }

        public Board()       { INIT(); }

        public Board(int[,] a)
        {
            try { 
                for(int i =0; i < 2;i++)
                {
                    for(int j = 0; j < 8; j++)
                    {
                        this.board[i, j] = a[i, j];
                    }
                }
            } catch(Exception e)
            {
                INIT();
            }
        }

        public Board Clone()
        {
            return new Board(this.board);
        }

        public void INIT()
        {
            board = new int[,]
            {   { 0, 6, 6, 6, 6, 6, 6, -1 },
                { -1, 6, 6, 6, 6, 6, 6, 0 }  };
        }

        public List<Board> getPossibleBoards(Board brd, int player)
        {
            
            List<Board> Ans = new List<Board>();
            int idy = player;
            if (idy < 0 || idy >= 2)
                return null;
            for( int idx = 1 ; idx < 7 ; idx++ )
            {
                if(brd.board[idy,idx] != 0)
                {
                    Board tmp = brd.Clone();
                    if(tmp.make_hod(idx, idy) == 1)
                    {
                        Ans.AddRange(getPossibleBoards(tmp, player));
                    }
                    else
                    {
                        Ans.Add(tmp);
                    }
                }
            }
            return Ans;
        }

        public int make_hod(int cell_number, int player)
        {
            // return 1 - если игрок повторяет ход
            int i = board[player, cell_number];

            if (i <= 0)
                return 1;

            board[player, cell_number] = 0;

            int incr = player == 0 ? -1 : 1;
            int idx = cell_number;
            int idy = player;
            while(i>0)
            {
                if (idx + incr > N || idx + incr < 0)
                {
                    idy = (idy + 1) % 2;
                    idx = idy == 0 ? 7 : 0;
                    incr *= -1;
                }
                if (player == 0 && idy == 1 && idx+incr == 7) {
                    idx+=incr;
                    continue;
                }
                else if(player == 1 && idy == 0 && idx+incr == 0) {
                    idx += incr;
                    continue;
                }
                idx+=incr;
                i--;
                board[idy, idx]++;
                
                if (i == 0 && board[idy,idx] == 1 && idy == player && board[(player + 1) % 2, idx] != -1)
                {
                    board[player, player == 0 ? 0 : 7] += board[(player + 1) % 2, idx ];
                    board[(player + 1) % 2, idx] = 0;
                }
                if (i == 0 && idy == player && idx == (player == 0 ? 0 : 7))
                    return 1;
                //else if(i==0)
                //    return 0;
            }


            int f = 0;
            int top_summ = 0;
            int bot_summ = 0;
            for(int j =1;j<7;j++)
            {
                top_summ += board[0, j];
                bot_summ += board[1, j];
            }
            if (top_summ == 0 && bot_summ == 0)
            {
                return 0;
            }else if (top_summ == 0)
            {
                for (int j = 1; j < 7; j++)
                    board[1, j] = 0;
                board[1, 7] += bot_summ;
            }
            else if (bot_summ == 0)
            {
                for (int j = 1; j < 7; j++)
                    board[0, j] = 0;
                board[0, 0] += top_summ;
            }

            return 0;
        }
        
        public int[,] get_board()
        {
            int[,] a = new int[2, 6];
            for (int i =0;i<2;i++)
            {
                for(int j =1;j<7;j++)
                {
                    a[i, j - 1] = this.board[i, j];
                }
            }
            return a;
        }

        public bool isTerm()
        {
            int f = 0;
            int top_summ = 0;
            int bot_summ = 0;
            for (int j = 1; j < 7; j++)
            {
                top_summ += board[0, j];
                bot_summ += board[1, j];
            }
            if (top_summ + bot_summ == 0)
                return true;
            return false;
        }

    }
}
