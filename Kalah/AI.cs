using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalah
{

    class logic
    {
        
        Board brd;
        int fx;
        public logic(Board new_brd, int new_fx)
        {
            this.brd = new_brd;
            this.fx = new_fx;
        }
    }

    class AI
    {
        const int EPIC_BIG_NUMBER = 1000000;
        int AI_CON = 0;
        // 0 - top
        // 1 - bot

        public Board brd;

        public AI() { }

        public AI(Board new_brd) { this.brd = new_brd; }

        public Board make_hod(int player, int depth)
        {
            List<logic> pos_moves = new List<logic>();
            List<Board> pos_boards = brd.getPossibleBoards(brd, player);

            if (pos_boards.Count == 0)
                return this.brd;

            int max = -10000;
            Board tmp = pos_boards[0];
            for(int i = 0; i < pos_boards.Count;i++)
            {
                int rating = max_f(pos_boards[i], (AI_CON + 1) % 2, depth, -EPIC_BIG_NUMBER, EPIC_BIG_NUMBER);
                pos_moves.Add(new logic(pos_boards[i], rating));
                if(max< rating)
                {
                    max = rating;
                    tmp = pos_boards[i];
                }
            }
            return tmp; 
        }
        public bool isTerm(Board a)
        {
            return a.isTerm();
        }

        public int max_f(Board a, int player, int depth, int alpha, int beta)
        {
            if (isTerm(a) || depth <=0 )
                return heuristic(a, AI_CON);
            
            List<Board> tmp = a.getPossibleBoards(a,player);

            int score = alpha;
            for(int i=0;i <tmp.Count;i++)
            {
                int buf = min_f(tmp[i],(player+1)%2,depth-1, score,beta);
                if (score < buf)
                    score = buf;
                if(score >= beta)
                    return score;
            }
            return score;
        }

        public int min_f(Board a, int player, int depth, int alpha, int beta)
        {
            if (isTerm(a) || depth <= 0)
                return heuristic(a, AI_CON);

            List<Board> tmp = a.getPossibleBoards(a, player);

            int score = beta;
            for(int i=0;i<tmp.Count;i++)
            {
                int buf = max_f(tmp[i], (player + 1) % 2, depth - 1,alpha, score);
                if (score > buf)
                    score = buf;
                if (score <= alpha)
                    return score;
            }
            return score;
            
        }

        public int heuristic(Board brd, int condition)
        {
            if (condition == 0)
                return brd.board[0, 0] - brd.board[1, 7];
            else
                return brd.board[1, 7] - brd.board[0, 0];
        }
    }
}
