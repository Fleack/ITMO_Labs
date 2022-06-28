#include "RCube.h"

void RCube::solve()
{
	if (Solved)
		return;
	cnt_rotates = 0;
	history = "";
	step_one(cnt_rotates, history);
	step_two(cnt_rotates, history);
	step_three(cnt_rotates, history);
	if (history == "ERROR!")
		return;
	step_four(cnt_rotates, history);
	step_five(cnt_rotates, history);
	step_six(cnt_rotates, history);
	step_seven(cnt_rotates, history);
	Solved = true;
}

void RCube::step_one(int& cnt_rotates, std::string& history)
{
	int top_face_rotates_cnt = 0;
	while (top_face_rotates_cnt < 4)
	{
		if (cube_colors[1][0][1] == 'w' && cube_colors[5][2][1] == 'r')
		{
			top_face_rotates_cnt = 0;
			rotate180(1);
			cnt_rotates++;
			history += "#180-1#";
		}
		else if (cube_colors[2][0][1] == 'b' && cube_colors[5][1][2] == 'r')
		{
			top_face_rotates_cnt = 0;
			rotate180(2);
			cnt_rotates++;
			history += "#180-3#";
		}
		else if (cube_colors[4][0][1] == 'g' && cube_colors[5][1][0] == 'r')
		{
			top_face_rotates_cnt = 0;
			rotate180(4);
			cnt_rotates++;
			history += "#180-5#";

		}
		else if (cube_colors[3][0][1] == 'y' && cube_colors[5][0][1] == 'r')
		{
			top_face_rotates_cnt = 0;
			rotate180(3);
			cnt_rotates++;
			history += "#180-4#";
		}
		else if (cube_colors[1][0][1] == 'r' && cube_colors[5][2][1] == 'w')
		{
			top_face_rotates_cnt = 0;
			rotate90_l(5);
			cnt_rotates++;
			history += "#90L-5#";

			rotate90_l(2);
			cnt_rotates++;
			history += "#90L-2#";

			rotate90_r(1);
			cnt_rotates++;
			history += "#90R-1#";

			rotate90_r(2);
			cnt_rotates++;
			history += "#90R-2#";
		}
		else if (cube_colors[2][0][1] == 'r' && cube_colors[5][1][2] == 'b')
		{
			top_face_rotates_cnt = 0;
			rotate90_l(5);
			cnt_rotates++;
			history += "#90L-5#";

			rotate90_l(3);
			cnt_rotates++;
			history += "#90L-3#";

			rotate90_r(2);
			cnt_rotates++;
			history += "#90R-2#";

			rotate90_r(3);
			cnt_rotates++;
			history += "#90R-3#";
		}
		else if (cube_colors[4][0][1] == 'r' && cube_colors[5][1][0] == 'g')
		{
			top_face_rotates_cnt = 0;
			rotate90_l(5);
			cnt_rotates++;
			history += "#90L-5#";

			rotate90_l(1);
			cnt_rotates++;
			history += "#90L-1#";

			rotate90_r(4);
			cnt_rotates++;
			history += "#90R-4#";

			rotate90_r(1);
			cnt_rotates++;
			history += "#90R-1#";
		}
		else if (cube_colors[3][0][1] == 'r' && cube_colors[5][0][1] == 'y')
		{
			top_face_rotates_cnt = 0;
			rotate90_l(5);
			cnt_rotates++;
			history += "#90L-5#";

			rotate90_l(4);
			cnt_rotates++;
			history += "#90L-4#";

			rotate90_r(3);
			cnt_rotates++;
			history += "#90R-3#";

			rotate90_r(4);
			cnt_rotates++;
			history += "#90R-4#";
		}
		else if (cube_colors[1][1][2] == 'r')
		{
			top_face_rotates_cnt = 0;
			rotate90_l(1);
			cnt_rotates++;
			history += "#90L-1#";

			rotate90_l(5);
			cnt_rotates++;
			history += "#90L-5#";

			rotate90_r(1);
			cnt_rotates++;
			history += "#90R-1#";
		}
		else if (cube_colors[2][1][2] == 'r')
		{
			top_face_rotates_cnt = 0;
			rotate90_l(2);
			cnt_rotates++;
			history += "#90L-2#";

			rotate90_l(5);
			cnt_rotates++;
			history += "#90L-5#";

			rotate90_r(2);
			cnt_rotates++;
			history += "#90R-2#";
		}
		else if (cube_colors[4][1][2] == 'r')
		{
			top_face_rotates_cnt = 0;
			rotate90_l(4);
			cnt_rotates++;
			history += "#90L-4#";

			rotate90_l(5);
			cnt_rotates++;
			history += "#90L-5#";

			rotate90_r(4);
			cnt_rotates++;
			history += "#90R-4#";
		}
		else if (cube_colors[3][1][2] == 'r')
		{
			top_face_rotates_cnt = 0;
			rotate90_l(3);
			cnt_rotates++;
			history += "#90L-3#";

			rotate90_l(5);
			cnt_rotates++;
			history += "#90L-5#";

			rotate90_r(3);
			cnt_rotates++;
			history += "#90R-3#";
		}
		else if (cube_colors[1][1][0] == 'r')
		{
			top_face_rotates_cnt = 0;
			rotate90_r(1);
			cnt_rotates++;
			history += "#90R-1#";

			rotate90_l(5);
			cnt_rotates++;
			history += "#90L-5#";

			rotate90_l(1);
			cnt_rotates++;
			history += "#90L-1#";
		}
		else if (cube_colors[2][1][0] == 'r')
		{
			top_face_rotates_cnt = 0;
			rotate90_r(2);
			cnt_rotates++;
			history += "#90R-2#";

			rotate90_l(5);
			cnt_rotates++;
			history += "#90L-5#";

			rotate90_l(2);
			cnt_rotates++;
			history += "#90L-2#";
		}
		else if (cube_colors[4][1][0] == 'r')
		{
			top_face_rotates_cnt = 0;
			rotate90_r(4);
			cnt_rotates++;
			history += "#90R-4#";

			rotate90_l(5);
			cnt_rotates++;
			history += "#90L-5#";

			rotate90_l(4);
			cnt_rotates++;
			history += "#90L-4#";
		}
		else if (cube_colors[3][1][0] == 'r')
		{
			top_face_rotates_cnt = 0;
			rotate90_r(3);
			cnt_rotates++;
			history += "#90R-3#";

			rotate90_l(5);
			cnt_rotates++;
			history += "#90L-5#";

			rotate90_l(3);
			cnt_rotates++;
			history += "#90L-3#";
		}
		else if (cube_colors[1][2][1] == 'r')
		{
			top_face_rotates_cnt = 0;
			rotate180(1);
			cnt_rotates++;
			history += "#180-1#";

			rotate90_l(5);
			cnt_rotates++;
			history += "#90L-5#";

			rotate180(1);
			cnt_rotates++;
			history += "#180-1#";
		}
		else if (cube_colors[2][2][1] == 'r')
		{
			top_face_rotates_cnt = 0;
			rotate180(2);
			cnt_rotates++;
			history += "#180-2#";

			rotate90_l(5);
			cnt_rotates++;
			history += "#90L-5#";

			rotate180(2);
			cnt_rotates++;
			history += "#180-2#";
		}
		else if (cube_colors[4][2][1] == 'r')
		{
			top_face_rotates_cnt = 0;
			rotate180(4);
			cnt_rotates++;
			history += "#180-4#";

			rotate90_l(5);
			cnt_rotates++;
			history += "#90L-5#";

			rotate180(4);
			cnt_rotates++;
			history += "#180-4#";
		}
		else if (cube_colors[3][2][1] == 'r')
		{
			top_face_rotates_cnt = 0;
			rotate180(3);
			cnt_rotates++;
			history += "#180-3#";

			rotate90_l(5);
			cnt_rotates++;
			history += "#90L-5#";

			rotate180(3);
			cnt_rotates++;
			history += "#180-3#";
		}
		else if (cube_colors[1][2][1] != 'w' && cube_colors[0][0][1] == 'r')
		{
			top_face_rotates_cnt = 0;
			rotate180(1);
			cnt_rotates++;
			history += "#180-1#";

			rotate90_l(5);
			cnt_rotates++;
			history += "#90L-5#";

			rotate180(1);
			cnt_rotates++;
			history += "#180-1#";
		}
		else if (cube_colors[2][2][1] != 'b' && cube_colors[0][1][2] == 'r')
		{
			top_face_rotates_cnt = 0;
			rotate180(2);
			cnt_rotates++;
			history += "#180-2#";

			rotate90_l(5);
			cnt_rotates++;
			history += "#90L-5#";

			rotate180(2);
			cnt_rotates++;
			history += "#180-2#";
		}
		else if (cube_colors[4][2][1] != 'g' && cube_colors[0][1][0] == 'r')
		{
			top_face_rotates_cnt = 0;
			rotate180(4);
			cnt_rotates++;
			history += "#180-4#";

			rotate90_l(5);
			cnt_rotates++;
			history += "#90L-5#";

			rotate180(4);
			cnt_rotates++;
			history += "#180-4#";
		}
		else if (cube_colors[3][2][1] != 'y' && cube_colors[0][2][1] == 'r')
		{
			top_face_rotates_cnt = 0;
			rotate180(3);
			cnt_rotates++;
			history += "#180-3#";

			rotate90_l(5);
			cnt_rotates++;
			history += "#90L-5#";

			rotate180(3);
			cnt_rotates++;
			history += "#180-3#";
		}
		else
		{
			top_face_rotates_cnt++;
			rotate90_r(5);
			cnt_rotates++;
			history += "#90R-5#";
		}
	}
	// ”дал¤ем последние 4 поворота верхней грани, так как они не вли¤ют на финальный результат
	history.erase(history.end() - 28, history.end());
	cnt_rotates -= 4;
}

void RCube::step_two(int& cnt_rotates, std::string& history)
{
	int top_face_rotates_cnt = 0;
	while (top_face_rotates_cnt < 4)
	{
		if (cube_colors[1][2][1] == 'w' && cube_colors[1][0][2] == 'r' && cube_colors[2][0][0] == 'b' && cube_colors[2][2][1] == 'b' && cube_colors[5][2][2] == 'w')
		{
			top_face_rotates_cnt = 0;
			rotate90_l(1);
			cnt_rotates++;
			history += "#90L-1#";

			rotate90_l(5);
			cnt_rotates++;
			history += "#90L-5#";

			rotate90_r(1);
			cnt_rotates++;
			history += "#90R-1#";
		}
		else if (cube_colors[1][2][1] == 'w' && cube_colors[1][0][0] == 'w' && cube_colors[4][0][2] == 'r' && cube_colors[4][2][1] == 'g' && cube_colors[5][2][0] == 'g')
		{
			top_face_rotates_cnt = 0;
			rotate90_l(4);
			cnt_rotates++;
			history += "#90L-4#";

			rotate90_l(5);
			cnt_rotates++;
			history += "#90L-5#";

			rotate90_r(4);
			cnt_rotates++;
			history += "#90R-4#";
		}
		else if (cube_colors[3][2][1] == 'y' && cube_colors[3][0][0] == 'y' && cube_colors[2][0][2] == 'r' && cube_colors[2][2][1] == 'b' && cube_colors[5][0][2] == 'b')
		{
			top_face_rotates_cnt = 0;
			rotate90_l(2);
			cnt_rotates++;
			history += "#90L-2#";

			rotate90_l(5);
			cnt_rotates++;
			history += "#90L-5#";

			rotate90_r(2);
			cnt_rotates++;
			history += "#90R-2#";
		}
		else if (cube_colors[3][2][1] == 'y' && cube_colors[3][0][2] == 'r' && cube_colors[4][0][0] == 'g' && cube_colors[4][2][1] == 'g' && cube_colors[5][0][0] == 'y')
		{
			top_face_rotates_cnt = 0;
			rotate90_l(3);
			cnt_rotates++;
			history += "#90L-3#";

			rotate90_l(5);
			cnt_rotates++;
			history += "#90L-5#";

			rotate90_r(3);
			cnt_rotates++;
			history += "#90R-3#";
		}
		else if (cube_colors[1][2][1] == 'w' && cube_colors[1][0][2] == 'w' && cube_colors[2][0][0] == 'r' && cube_colors[2][2][1] == 'b' && cube_colors[5][2][2] == 'b')
		{
			top_face_rotates_cnt = 0;
			rotate90_r(2);
			cnt_rotates++;
			history += "#90R-2#";

			rotate90_r(5);
			cnt_rotates++;
			history += "#90R-5#";

			rotate90_l(2);
			cnt_rotates++;
			history += "#90L-2#";
		}
		else if (cube_colors[1][0][0] == 'r' && cube_colors[1][2][1] == 'w' && cube_colors[4][2][1] == 'g' && cube_colors[4][0][2] == 'g' && cube_colors[5][2][0] == 'w')
		{
			top_face_rotates_cnt = 0;
			rotate90_r(1);
			cnt_rotates++;
			history += "#90R-1#";

			rotate90_r(5);
			cnt_rotates++;
			history += "#90R-5#";

			rotate90_l(1);
			cnt_rotates++;
			history += "#90L-1#";
		}
		else if (cube_colors[3][2][1] == 'y' && cube_colors[3][0][0] == 'r' && cube_colors[2][0][2] == 'b' && cube_colors[2][2][1] == 'b' && cube_colors[5][0][2] == 'y')
		{
			top_face_rotates_cnt = 0;
			rotate90_r(3);
			cnt_rotates++;
			history += "#90R-3#";

			rotate90_r(5);
			cnt_rotates++;
			history += "#90R-5#";

			rotate90_l(3);
			cnt_rotates++;
			history += "#90L-3#";
		}
		else if (cube_colors[3][2][1] == 'y' && cube_colors[3][0][2] == 'y' && cube_colors[4][0][0] == 'r' && cube_colors[4][2][1] == 'g' && cube_colors[5][0][0] == 'g')
		{
			top_face_rotates_cnt = 0;
			rotate90_r(4);
			cnt_rotates++;
			history += "#90R-4#";

			rotate90_r(5);
			cnt_rotates++;
			history += "#90R-5#";

			rotate90_l(4);
			cnt_rotates++;
			history += "#90L-4#";
		}
		else if (cube_colors[1][2][1] == 'w' && cube_colors[1][0][2] == 'b' && cube_colors[2][0][0] == 'w' && cube_colors[2][2][1] == 'b' && cube_colors[5][2][2] == 'r')
		{
			top_face_rotates_cnt = 0;
			rotate90_r(2);
			cnt_rotates++;
			history += "#90R-2#";

			rotate90_l(5);
			cnt_rotates++;
			history += "#90L-5#";

			rotate90_l(2);
			cnt_rotates++;
			history += "#90L-2#";

			rotate180(5);
			cnt_rotates++;
			history += "#180-5#";

			rotate90_r(2);
			cnt_rotates++;
			history += "#90R-2#";

			rotate90_r(5);
			cnt_rotates++;
			history += "#90R-5#";

			rotate90_l(2);
			cnt_rotates++;
			history += "#90L-2#";
		}
		else if (cube_colors[1][0][0] == 'g' && cube_colors[1][2][1] == 'w' && cube_colors[4][2][1] == 'g' && cube_colors[4][0][2] == 'w' && cube_colors[5][2][0] == 'r')
		{
			top_face_rotates_cnt = 0;
			rotate90_r(1);
			cnt_rotates++;
			history += "#90R-1#";

			rotate90_l(5);
			cnt_rotates++;
			history += "#90L-5#";

			rotate90_l(1);
			cnt_rotates++;
			history += "#90L-1#";

			rotate180(5);
			cnt_rotates++;
			history += "#180-5#";

			rotate90_r(1);
			cnt_rotates++;
			history += "#90R-1#";

			rotate90_r(5);
			cnt_rotates++;
			history += "#90R-5#";

			rotate90_l(1);
			cnt_rotates++;
			history += "#90L-1#";
		}
		else if (cube_colors[3][2][1] == 'y' && cube_colors[3][0][0] == 'b' && cube_colors[2][0][2] == 'y' && cube_colors[2][2][1] == 'b' && cube_colors[5][0][2] == 'r')
		{
			top_face_rotates_cnt = 0;
			rotate90_r(3);
			cnt_rotates++;
			history += "#90R-3#";

			rotate90_l(5);
			cnt_rotates++;
			history += "#90L-5#";

			rotate90_l(3);
			cnt_rotates++;
			history += "#90L-3#";

			rotate180(5);
			cnt_rotates++;
			history += "#180-5#";

			rotate90_r(3);
			cnt_rotates++;
			history += "#90R-3#";

			rotate90_r(5);
			cnt_rotates++;
			history += "#90R-5#";

			rotate90_l(3);
			cnt_rotates++;
			history += "#90L-3#";
		}
		else if (cube_colors[3][2][1] == 'y' && cube_colors[3][0][2] == 'g' && cube_colors[4][0][0] == 'y' && cube_colors[4][2][1] == 'g' && cube_colors[5][0][0] == 'r')
		{
			top_face_rotates_cnt = 0;
			rotate90_r(4);
			cnt_rotates++;
			history += "#90R-4#";

			rotate90_l(5);
			cnt_rotates++;
			history += "#90L-5#";

			rotate90_l(4);
			cnt_rotates++;
			history += "#90L-4#";

			rotate180(5);
			cnt_rotates++;
			history += "#180-5#";

			rotate90_r(4);
			cnt_rotates++;
			history += "#90R-4#";

			rotate90_r(5);
			cnt_rotates++;
			history += "#90R-5#";

			rotate90_l(4);
			cnt_rotates++;
			history += "#90L-4#";
		}
		else if (cube_colors[1][2][1] == 'w' && cube_colors[1][2][2] == 'r' && cube_colors[2][2][1] == 'b')
		{
			top_face_rotates_cnt = 0;
			rotate90_l(1);
			cnt_rotates++;
			history += "#90L-1#";

			rotate90_l(5);
			cnt_rotates++;
			history += "#90L-5#";

			rotate90_r(1);
			cnt_rotates++;
			history += "#90R-1#";
		}
		else if (cube_colors[1][2][1] == 'w' && cube_colors[4][2][1] == 'g' && cube_colors[4][2][2] == 'r')
		{
			top_face_rotates_cnt = 0;
			rotate90_l(4);
			cnt_rotates++;
			history += "#90L-4#";

			rotate90_l(5);
			cnt_rotates++;
			history += "#90L-5#";

			rotate90_r(4);
			cnt_rotates++;
			history += "#90R-4#";
		}
		else if (cube_colors[3][2][1] == 'y' && cube_colors[2][2][1] == 'b' && cube_colors[2][2][2] == 'r')
		{
			top_face_rotates_cnt = 0;
			rotate90_l(2);
			cnt_rotates++;
			history += "#90L-2#";

			rotate90_l(5);
			cnt_rotates++;
			history += "#90L-5#";

			rotate90_r(2);
			cnt_rotates++;
			history += "#90R-2#";
		}
		else if (cube_colors[3][2][1] == 'y' && cube_colors[3][2][2] == 'r' && cube_colors[4][2][1] == 'g')
		{
			top_face_rotates_cnt = 0;
			rotate90_l(3);
			cnt_rotates++;
			history += "#90L-3#";

			rotate90_l(5);
			cnt_rotates++;
			history += "#90L-5#";

			rotate90_r(3);
			cnt_rotates++;
			history += "#90R-3#";
		}
		else if (cube_colors[1][2][1] == 'w' && cube_colors[2][2][0] == 'r' && cube_colors[2][2][1] == 'b')
		{
			top_face_rotates_cnt = 0;
			rotate90_l(1);
			cnt_rotates++;
			history += "#90L-1#";

			rotate90_l(5);
			cnt_rotates++;
			history += "#90L-5#";

			rotate90_r(1);
			cnt_rotates++;
			history += "#90R-1#";
		}
		else if (cube_colors[1][2][1] == 'w' && cube_colors[1][2][0] == 'r' && cube_colors[4][2][1] == 'g')
		{
			top_face_rotates_cnt = 0;
			rotate90_l(4);
			cnt_rotates++;
			history += "#90L-4#";

			rotate90_l(5);
			cnt_rotates++;
			history += "#90L-5#";

			rotate90_r(4);
			cnt_rotates++;
			history += "#90R-4#";
		}
		else if (cube_colors[3][2][1] == 'y' && cube_colors[3][2][0] == 'r' && cube_colors[2][2][1] == 'b')
		{
			top_face_rotates_cnt = 0;
			rotate90_l(2);
			cnt_rotates++;
			history += "#90L-2#";

			rotate90_l(5);
			cnt_rotates++;
			history += "#90L-5#";

			rotate90_r(2);
			cnt_rotates++;
			history += "#90R-2#";
		}
		else if (cube_colors[3][2][1] == 'y' && cube_colors[4][2][0] == 'r' && cube_colors[4][2][1] == 'g')
		{
			top_face_rotates_cnt = 0;
			rotate90_l(3);
			cnt_rotates++;
			history += "#90L-3#";

			rotate90_l(5);
			cnt_rotates++;
			history += "#90L-5#";

			rotate90_r(3);
			cnt_rotates++;
			history += "#90R-3#";
		}
		else if (cube_colors[0][0][0] == 'r' && cube_colors[0][0][1] == 'r' && cube_colors[0][1][0] == 'r' && cube_colors[0][1][1] == 'r' && cube_colors[0][1][2] == 'r' && cube_colors[0][2][1] == 'r' && cube_colors[1][2][1] == 'w' && cube_colors[4][2][1] == 'g' && (cube_colors[4][2][2] != 'g' || cube_colors[1][2][0] != 'w'))
		{
			top_face_rotates_cnt = 0;
			rotate90_r(1);
			cnt_rotates++;
			history += "#90R-1#";

			rotate90_l(5);
			cnt_rotates++;
			history += "#90L-5#";

			rotate90_l(1);
			cnt_rotates++;
			history += "#90L-1#";
		}
		else if (cube_colors[0][0][2] == 'r' && cube_colors[0][0][1] == 'r' && cube_colors[0][1][0] == 'r' && cube_colors[0][1][1] == 'r' && cube_colors[0][1][2] == 'r' && cube_colors[0][2][1] == 'r' && cube_colors[1][2][1] == 'w' && cube_colors[2][2][1] == 'b' && (cube_colors[2][2][0] != 'b' || cube_colors[1][2][2] != 'w'))
		{
			top_face_rotates_cnt = 0;
			rotate90_r(2);
			cnt_rotates++;
			history += "#90R-2#";

			rotate90_l(5);
			cnt_rotates++;
			history += "#90L-5#";

			rotate90_l(2);
			cnt_rotates++;
			history += "#90L-2#";
		}
		else if (cube_colors[0][0][1] == 'r' && cube_colors[0][1][0] == 'r' && cube_colors[0][1][1] == 'r' && cube_colors[0][1][2] == 'r' && cube_colors[0][2][2] == 'r' && cube_colors[0][2][1] == 'r' && cube_colors[3][2][1] == 'y' && cube_colors[2][2][1] == 'b' && (cube_colors[3][2][0] != 'y' || cube_colors[2][2][2] != 'b'))
		{
			top_face_rotates_cnt = 0;
			rotate90_r(3);
			cnt_rotates++;
			history += "#90R-3#";

			rotate90_l(5);
			cnt_rotates++;
			history += "#90L-5#";

			rotate90_l(3);
			cnt_rotates++;
			history += "#90L-3#";
		}
		else if (cube_colors[0][0][1] == 'r' && cube_colors[0][1][0] == 'r' && cube_colors[0][1][1] == 'r' && cube_colors[0][1][2] == 'r' && cube_colors[0][2][0] == 'r' && cube_colors[0][2][1] == 'r' && cube_colors[3][2][1] == 'y' && cube_colors[4][2][1] == 'g' && (cube_colors[3][2][2] != 'y' || cube_colors[4][2][0] != 'g'))
		{
			top_face_rotates_cnt = 0;
			rotate90_r(4);
			cnt_rotates++;
			history += "#90R-4#";

			rotate90_l(5);
			cnt_rotates++;
			history += "#90L-5#";

			rotate90_l(4);
			cnt_rotates++;
			history += "#90L-4#";
		}
		else
		{
			top_face_rotates_cnt++;
			rotate90_r(5);
			cnt_rotates++;
			history += "#90R-5#";
		}
	}
	// ”дал¤ем последние 4 поворота верхней грани, так как они не вли¤ют на финальный результат
	history.erase(history.end() - 28, history.end());
	cnt_rotates -= 4;
}

void RCube::step_three(int& cnt_rotates, std::string& history)
{
	int top_face_rotates_cnt;
	bool step_three_finished = false;
	while (!step_three_finished)
	{
		if (cnt_rotates > 30000)
		{
			history = "ERROR!";
			cnt_rotates = -1;
		}
		top_face_rotates_cnt = 0;
		while (top_face_rotates_cnt < 4)
		{
			if (cube_colors[1][2][0] == 'w' && cube_colors[1][2][1] == 'w' && cube_colors[1][2][2] == 'w' && cube_colors[1][0][1] == 'w' && cube_colors[4][2][0] == 'g' && cube_colors[4][2][1] == 'g' && cube_colors[4][2][2] == 'g' && cube_colors[5][2][1] == 'g')
			{
				top_face_rotates_cnt = 0;
				rotate90_l(5);
				cnt_rotates++;
				history += "#90L-5#";

				rotate90_l(4);
				cnt_rotates++;
				history += "#90L-4#";

				rotate90_r(5);
				cnt_rotates++;
				history += "#90R-5#";

				rotate90_r(4);
				cnt_rotates++;
				history += "#90R-4#";

				rotate90_r(5);
				cnt_rotates++;
				history += "#90R-5#";

				rotate90_r(1);
				cnt_rotates++;
				history += "#90R-1#";

				rotate90_l(5);
				cnt_rotates++;
				history += "#90L-5#";

				rotate90_l(1);
				cnt_rotates++;
				history += "#90L-1#";
			}
			else if (cube_colors[1][2][0] == 'w' && cube_colors[1][2][1] == 'w' && cube_colors[1][2][2] == 'w' && cube_colors[5][1][2] == 'w' && cube_colors[2][2][0] == 'b' && cube_colors[2][2][1] == 'b' && cube_colors[2][2][2] == 'b' && cube_colors[2][0][1] == 'b')
			{
				top_face_rotates_cnt = 0;
				rotate90_l(5);
				cnt_rotates++;
				history += "#90L-5#";

				rotate90_l(1);
				cnt_rotates++;
				history += "#90L-1#";

				rotate90_r(5);
				cnt_rotates++;
				history += "#90R-5#";

				rotate90_r(1);
				cnt_rotates++;
				history += "#90R-1#";

				rotate90_r(5);
				cnt_rotates++;
				history += "#90R-5#";

				rotate90_r(2);
				cnt_rotates++;
				history += "#90R-2#";

				rotate90_l(5);
				cnt_rotates++;
				history += "#90L-5#";

				rotate90_l(2);
				cnt_rotates++;
				history += "#90L-2#";
			}
			else if (cube_colors[3][2][0] == 'y' && cube_colors[3][2][1] == 'y' && cube_colors[3][2][2] == 'y' && cube_colors[3][0][1] == 'y' && cube_colors[2][2][0] == 'b' && cube_colors[2][2][1] == 'b' && cube_colors[2][2][2] == 'b' && cube_colors[5][0][1] == 'b')
			{
				top_face_rotates_cnt = 0;
				rotate90_l(5);
				cnt_rotates++;
				history += "#90L-5#";

				rotate90_l(2);
				cnt_rotates++;
				history += "#90L-2#";

				rotate90_r(5);
				cnt_rotates++;
				history += "#90R-5#";

				rotate90_r(2);
				cnt_rotates++;
				history += "#90R-2#";

				rotate90_r(5);
				cnt_rotates++;
				history += "#90R-5#";

				rotate90_r(3);
				cnt_rotates++;
				history += "#90R-3#";

				rotate90_l(5);
				cnt_rotates++;
				history += "#90L-5#";

				rotate90_l(3);
				cnt_rotates++;
				history += "#90L-3#";
			}
			else if (cube_colors[3][2][0] == 'y' && cube_colors[3][2][1] == 'y' && cube_colors[3][2][2] == 'y' && cube_colors[5][1][0] == 'y' && cube_colors[4][2][0] == 'g' && cube_colors[4][2][1] == 'g' && cube_colors[4][2][2] == 'g' && cube_colors[4][0][1] == 'g')
			{
				top_face_rotates_cnt = 0;
				rotate90_l(5);
				cnt_rotates++;
				history += "#90L-5#";

				rotate90_l(3);
				cnt_rotates++;
				history += "#90L-3#";

				rotate90_r(5);
				cnt_rotates++;
				history += "#90R-5#";

				rotate90_r(3);
				cnt_rotates++;
				history += "#90R-3#";

				rotate90_r(5);
				cnt_rotates++;
				history += "#90R-5#";

				rotate90_r(4);
				cnt_rotates++;
				history += "#90R-4#";

				rotate90_l(5);
				cnt_rotates++;
				history += "#90L-5#";

				rotate90_l(4);
				cnt_rotates++;
				history += "#90L-4#";
			}
			else if (cube_colors[1][2][0] == 'w' && cube_colors[1][2][1] == 'w' && cube_colors[1][2][2] == 'w' && cube_colors[1][0][1] == 'w' && cube_colors[2][2][0] == 'g' && cube_colors[2][2][1] == 'g' && cube_colors[2][2][2] == 'g' && cube_colors[5][2][1] == 'g')
			{
				top_face_rotates_cnt = 0;
				rotate90_r(5);
				cnt_rotates++;
				history += "#90R-5#";

				rotate90_r(2);
				cnt_rotates++;
				history += "#90R-2#";

				rotate90_l(5);
				cnt_rotates++;
				history += "#90L-5#";

				rotate90_l(2);
				cnt_rotates++;
				history += "#90L-2#";

				rotate90_l(5);
				cnt_rotates++;
				history += "#90L-5#";

				rotate90_l(1);
				cnt_rotates++;
				history += "#90L-1#";

				rotate90_r(5);
				cnt_rotates++;
				history += "#90R-5#";

				rotate90_r(1);
				cnt_rotates++;
				history += "#90R-1#";
			}
			else if (cube_colors[1][2][0] == 'w' && cube_colors[1][2][1] == 'w' && cube_colors[1][2][2] == 'w' && cube_colors[5][1][0] == 'w' && cube_colors[4][2][0] == 'g' && cube_colors[4][2][1] == 'g' && cube_colors[4][2][2] == 'g' && cube_colors[4][0][1] == 'g')
			{
				top_face_rotates_cnt = 0;
				rotate90_r(5);
				cnt_rotates++;
				history += "#90R-5#";

				rotate90_r(1);
				cnt_rotates++;
				history += "#90R-1#";

				rotate90_l(5);
				cnt_rotates++;
				history += "#90L-5#";

				rotate90_l(1);
				cnt_rotates++;
				history += "#90L-1#";

				rotate90_l(5);
				cnt_rotates++;
				history += "#90L-5#";

				rotate90_l(4);
				cnt_rotates++;
				history += "#90L-4#";

				rotate90_r(5);
				cnt_rotates++;
				history += "#90R-5#";

				rotate90_r(4);
				cnt_rotates++;
				history += "#90R-4#";
			}
			else if (cube_colors[3][2][0] == 'y' && cube_colors[3][2][1] == 'y' && cube_colors[3][2][2] == 'y' && cube_colors[3][0][1] == 'y' && cube_colors[4][2][0] == 'g' && cube_colors[4][2][1] == 'g' && cube_colors[4][2][2] == 'g' && cube_colors[5][0][1] == 'g')
			{
				top_face_rotates_cnt = 0;
				rotate90_r(5);
				cnt_rotates++;
				history += "#90R-5#";

				rotate90_r(4);
				cnt_rotates++;
				history += "#90R-4#";

				rotate90_l(5);
				cnt_rotates++;
				history += "#90L-5#";

				rotate90_l(4);
				cnt_rotates++;
				history += "#90L-4#";

				rotate90_l(5);
				cnt_rotates++;
				history += "#90L-5#";

				rotate90_l(3);
				cnt_rotates++;
				history += "#90L-3#";

				rotate90_r(5);
				cnt_rotates++;
				history += "#90R-5#";

				rotate90_r(3);
				cnt_rotates++;
				history += "#90R-3#";
			}
			else if (cube_colors[3][2][0] == 'y' && cube_colors[3][2][1] == 'y' && cube_colors[3][2][2] == 'y' && cube_colors[5][1][2] == 'y' && cube_colors[2][2][0] == 'b' && cube_colors[2][2][1] == 'b' && cube_colors[2][2][2] == 'b' && cube_colors[2][0][1] == 'b')
			{
				top_face_rotates_cnt = 0;
				rotate90_r(5);
				cnt_rotates++;
				history += "#90R-5#";

				rotate90_r(3);
				cnt_rotates++;
				history += "#90R-3#";

				rotate90_l(5);
				cnt_rotates++;
				history += "#90L-5#";

				rotate90_l(3);
				cnt_rotates++;
				history += "#90L-3#";

				rotate90_l(5);
				cnt_rotates++;
				history += "#90L-5#";

				rotate90_l(2);
				cnt_rotates++;
				history += "#90L-2#";

				rotate90_r(5);
				cnt_rotates++;
				history += "#90R-5#";

				rotate90_r(2);
				cnt_rotates++;
				history += "#90R-2#";
			}
			else
			{
				top_face_rotates_cnt++;
				rotate90_r(5);
				cnt_rotates++;
				history += "#90L-5#";
			}
		}
		// ”дал¤ем последние 4 поворота верхней грани, так как они не вли¤ют на финальный результат
		history.erase(history.end() - 28, history.end());
		cnt_rotates -= 4;
		// ѕроверка после 4 поворотов верхней грани, если хоть один выполн¤етс¤, начинаем третий шаг снова
		if (cube_colors[1][2][0] == 'w' && cube_colors[1][2][1] == 'w' && cube_colors[1][2][2] == 'w' && cube_colors[2][2][0] == 'b' && cube_colors[2][2][1] == 'b' && cube_colors[2][2][2] == 'b' && (cube_colors[2][1][0] != 'b' || cube_colors[1][1][2] != 'w'))
		{
			rotate90_r(5);
			cnt_rotates++;
			history += "#90R-5#";

			rotate90_r(2);
			cnt_rotates++;
			history += "#90R-2#";

			rotate90_l(5);
			cnt_rotates++;
			history += "#90L-5#";

			rotate90_l(2);
			cnt_rotates++;
			history += "#90L-2#";

			rotate90_l(5);
			cnt_rotates++;
			history += "#90L-5#";

			rotate90_l(1);
			cnt_rotates++;
			history += "#90L-1#";

			rotate90_r(5);
			cnt_rotates++;
			history += "#90R-5#";

			rotate90_r(1);
			cnt_rotates++;
			history += "#90R-1#";
		}
		else if (cube_colors[1][2][0] == 'w' && cube_colors[1][2][1] == 'w' && cube_colors[1][2][2] == 'w' && cube_colors[4][2][0] == 'g' && cube_colors[4][2][1] == 'g' && cube_colors[4][2][2] == 'g' && (cube_colors[4][1][2] != 'g' || cube_colors[1][1][0] != 'w'))
		{
			rotate90_r(5);
			cnt_rotates++;
			history += "#90R-5#";

			rotate90_r(1);
			cnt_rotates++;
			history += "#90R-1#";

			rotate90_l(5);
			cnt_rotates++;
			history += "#90L-5#";

			rotate90_l(1);
			cnt_rotates++;
			history += "#90L-1#";

			rotate90_l(5);
			cnt_rotates++;
			history += "#90L-5#";

			rotate90_l(4);
			cnt_rotates++;
			history += "#90L-4#";

			rotate90_r(5);
			cnt_rotates++;
			history += "#90R-5#";

			rotate90_r(4);
			cnt_rotates++;
			history += "#90R-4#";
		}
		else if (cube_colors[3][2][0] == 'y' && cube_colors[3][2][1] == 'y' && cube_colors[3][2][2] == 'y' && cube_colors[4][2][0] == 'g' && cube_colors[4][2][1] == 'g' && cube_colors[4][2][2] == 'g' && (cube_colors[4][1][0] != 'g' || cube_colors[3][1][2] != 'y'))
		{
			rotate90_r(5);
			cnt_rotates++;
			history += "#90R-5#";

			rotate90_r(4);
			cnt_rotates++;
			history += "#90R-4#";

			rotate90_l(5);
			cnt_rotates++;
			history += "#90L-5#";

			rotate90_l(4);
			cnt_rotates++;
			history += "#90L-4#";

			rotate90_l(5);
			cnt_rotates++;
			history += "#90L-5#";

			rotate90_l(3);
			cnt_rotates++;
			history += "#90L-3#";

			rotate90_r(5);
			cnt_rotates++;
			history += "#90R-5#";

			rotate90_r(3);
			cnt_rotates++;
			history += "#90R-3#";
		}
		else if (cube_colors[3][2][0] == 'y' && cube_colors[3][2][1] == 'y' && cube_colors[3][2][2] == 'y' && cube_colors[2][2][0] == 'b' && cube_colors[2][2][1] == 'b' && cube_colors[2][2][2] == 'b' && (cube_colors[2][1][2] != 'b' || cube_colors[3][1][0] != 'y'))
		{
			rotate90_r(5);
			cnt_rotates++;
			history += "#90R-5#";

			rotate90_r(3);
			cnt_rotates++;
			history += "#90R-3#";

			rotate90_l(5);
			cnt_rotates++;
			history += "#90L-5#";

			rotate90_l(3);
			cnt_rotates++;
			history += "#90L-3#";

			rotate90_l(5);
			cnt_rotates++;
			history += "#90L-5#";

			rotate90_l(2);
			cnt_rotates++;
			history += "#90L-2#";

			rotate90_r(5);
			cnt_rotates++;
			history += "#90R-5#";

			rotate90_r(2);
			cnt_rotates++;
			history += "#90R-2#";
		}
		else
		{
			step_three_finished = true;
		}
	}
}

void RCube::step_four(int& cnt_rotates, std::string& history)
{
	if (cube_colors[1][0][1] == 'o' && cube_colors[2][0][1] == 'o' && cube_colors[5][0][1] == 'o' && cube_colors[5][1][0] == 'o')
	{
		rotate90_r(1);
		cnt_rotates++;
		history += "#90R-1#";

		rotate90_r(5);
		cnt_rotates++;
		history += "#90R-5#";

		rotate90_r(2);
		cnt_rotates++;
		history += "#90R-2#";

		rotate90_l(5);
		cnt_rotates++;
		history += "#90L-5#";

		rotate90_l(2);
		cnt_rotates++;
		history += "#90L-2#";

		rotate90_l(1);
		cnt_rotates++;
		history += "#90L-1#";
	}
	else if (cube_colors[4][0][1] == 'o' && cube_colors[1][0][1] == 'o' && cube_colors[5][0][1] == 'o' && cube_colors[5][1][2] == 'o')
	{
		rotate90_r(4);
		cnt_rotates++;
		history += "#90R-4#";

		rotate90_r(5);
		cnt_rotates++;
		history += "#90R-5#";

		rotate90_r(1);
		cnt_rotates++;
		history += "#90R-1#";

		rotate90_l(5);
		cnt_rotates++;
		history += "#90L-5#";

		rotate90_l(1);
		cnt_rotates++;
		history += "#90L-1#";

		rotate90_l(4);
		cnt_rotates++;
		history += "#90L-4#";
	}
	else if (cube_colors[2][0][1] == 'o' && cube_colors[3][0][1] == 'o' && cube_colors[5][1][0] == 'o' && cube_colors[5][2][1] == 'o')
	{
		rotate90_r(2);
		cnt_rotates++;
		history += "#90R-2#";

		rotate90_r(5);
		cnt_rotates++;
		history += "#90R-5#";

		rotate90_r(3);
		cnt_rotates++;
		history += "#90R-3#";

		rotate90_l(5);
		cnt_rotates++;
		history += "#90L-5#";

		rotate90_l(3);
		cnt_rotates++;
		history += "#90L-3#";

		rotate90_l(2);
		cnt_rotates++;
		history += "#90L-2#";
	}
	else if (cube_colors[3][0][1] == 'o' && cube_colors[4][0][1] == 'o' && cube_colors[5][1][2] == 'o' && cube_colors[5][2][1] == 'o')
	{
		rotate90_r(3);
		cnt_rotates++;
		history += "#90R-3#";

		rotate90_r(5);
		cnt_rotates++;
		history += "#90R-5#";

		rotate90_r(4);
		cnt_rotates++;
		history += "#90R-4#";

		rotate90_l(5);
		cnt_rotates++;
		history += "#90L-5#";

		rotate90_l(4);
		cnt_rotates++;
		history += "#90L-4#";

		rotate90_l(3);
		cnt_rotates++;
		history += "#90L-3#";
	}
	else if (cube_colors[1][0][1] == 'o' && cube_colors[5][1][0] == 'o' && cube_colors[5][1][2] == 'o')
	{
		rotate90_r(1);
		cnt_rotates++;
		history += "#90R-1#";

		rotate90_r(2);
		cnt_rotates++;
		history += "#90R-2#";

		rotate90_r(5);
		cnt_rotates++;
		history += "#90R-5#";

		rotate90_l(2);
		cnt_rotates++;
		history += "#90L-2#";

		rotate90_l(5);
		cnt_rotates++;
		history += "#90L-5#";

		rotate90_l(1);
		cnt_rotates++;
		history += "#90L-1#";
	}
	else if (cube_colors[2][0][1] == 'o' && cube_colors[5][0][1] == 'o' && cube_colors[5][2][1] == 'o')
	{
		rotate90_r(2);
		cnt_rotates++;
		history += "#90R-2#";

		rotate90_r(3);
		cnt_rotates++;
		history += "#90R-3#";

		rotate90_r(5);
		cnt_rotates++;
		history += "#90R-5#";

		rotate90_l(3);
		cnt_rotates++;
		history += "#90L-3#";

		rotate90_l(5);
		cnt_rotates++;
		history += "#90L-5#";

		rotate90_l(2);
		cnt_rotates++;
		history += "#90L-2#";
	}
	else if (cube_colors[1][0][1] == 'o' && cube_colors[2][0][1] == 'o')
	{
		rotate90_r(1);
		cnt_rotates++;
		history += "#90R-1#";

		rotate90_r(5);
		cnt_rotates++;
		history += "#90R-5#";

		rotate90_r(2);
		cnt_rotates++;
		history += "#90R-2#";

		rotate90_l(5);
		cnt_rotates++;
		history += "#90L-5#";

		rotate90_l(2);
		cnt_rotates++;
		history += "#90L-2#";

		rotate90_l(1);
		cnt_rotates++;
		history += "#90L-1#";

		rotate90_r(2);
		cnt_rotates++;
		history += "#90R-2#";

		rotate90_r(3);
		cnt_rotates++;
		history += "#90R-3#";

		rotate90_r(5);
		cnt_rotates++;
		history += "#90R-5#";

		rotate90_l(3);
		cnt_rotates++;
		history += "#90L-3#";

		rotate90_l(5);
		cnt_rotates++;
		history += "#90L-5#";

		rotate90_l(2);
		cnt_rotates++;
		history += "#90L-2#";
	}
}

void RCube::step_five(int& cnt_rotates, std::string& history)
{
	if (cube_colors[2][0][1] == 'w')
	{
		rotate90_r(5);
		cnt_rotates++;
		history += "#90R-5#";

		rotate90_l(4);
		cnt_rotates++;
		history += "#90L-4#";

		rotate180(5);
		cnt_rotates++;
		history += "#180-5#";

		rotate90_r(4);
		cnt_rotates++;
		history += "#90R-4#";

		rotate90_r(5);
		cnt_rotates++;
		history += "#90R-5#";

		rotate90_l(4);
		cnt_rotates++;
		history += "#90L-4#";

		rotate90_r(5);
		cnt_rotates++;
		history += "#90R-5#";

		rotate90_r(4);
		cnt_rotates++;
		history += "#90R-4#";
	}
	else if (cube_colors[4][0][1] == 'w')
	{
		rotate90_r(5);
		cnt_rotates++;
		history += "#90R-5#";

		rotate90_l(3);
		cnt_rotates++;
		history += "#90L-3#";

		rotate180(5);
		cnt_rotates++;
		history += "#180-5#";

		rotate90_r(3);
		cnt_rotates++;
		history += "#90R-3#";

		rotate90_r(5);
		cnt_rotates++;
		history += "#90R-5#";

		rotate90_l(3);
		cnt_rotates++;
		history += "#90L-3#";

		rotate90_r(5);
		cnt_rotates++;
		history += "#90R-5#";

		rotate90_r(3);
		cnt_rotates++;
		history += "#90R-3#";
	}
	else if (cube_colors[3][0][1] == 'w')
	{
		rotate90_r(5);
		cnt_rotates++;
		history += "#90R-5#";

		rotate90_l(2);
		cnt_rotates++;
		history += "#90L-2#";

		rotate180(5);
		cnt_rotates++;
		history += "#180-5#";

		rotate90_r(2);
		cnt_rotates++;
		history += "#90R-2#";

		rotate90_r(5);
		cnt_rotates++;
		history += "#90R-5#";

		rotate90_l(2);
		cnt_rotates++;
		history += "#90L-2#";

		rotate90_r(5);
		cnt_rotates++;
		history += "#90R-5#";

		rotate90_r(2);
		cnt_rotates++;
		history += "#90R-2#";

		rotate90_r(5);
		cnt_rotates++;
		history += "#90R-5#";

		rotate90_l(3);
		cnt_rotates++;
		history += "#90L-3#";

		rotate180(5);
		cnt_rotates++;
		history += "#180-5#";

		rotate90_r(3);
		cnt_rotates++;
		history += "#90R-3#";

		rotate90_r(5);
		cnt_rotates++;
		history += "#90R-5#";

		rotate90_l(3);
		cnt_rotates++;
		history += "#90L-3#";

		rotate90_r(5);
		cnt_rotates++;
		history += "#90R-5#";

		rotate90_r(3);
		cnt_rotates++;
		history += "#90R-3#";

		rotate90_r(5);
		cnt_rotates++;
		history += "#90R-5#";

		rotate90_l(2);
		cnt_rotates++;
		history += "#90L-2#";

		rotate180(5);
		cnt_rotates++;
		history += "#180-5#";

		rotate90_r(2);
		cnt_rotates++;
		history += "#90R-2#";

		rotate90_r(5);
		cnt_rotates++;
		history += "#90R-5#";

		rotate90_l(2);
		cnt_rotates++;
		history += "#90L-2#";

		rotate90_r(5);
		cnt_rotates++;
		history += "#90R-5#";

		rotate90_r(2);
		cnt_rotates++;
		history += "#90R-2#";
	}
	if (cube_colors[3][0][1] == 'b')
	{
		rotate90_r(5);
		cnt_rotates++;
		history += "#90R-5#";

		rotate90_l(1);
		cnt_rotates++;
		history += "#90L-1#";

		rotate180(5);
		cnt_rotates++;
		history += "#180-5#";

		rotate90_r(1);
		cnt_rotates++;
		history += "#90R-1#";

		rotate90_r(5);
		cnt_rotates++;
		history += "#90R-5#";

		rotate90_l(1);
		cnt_rotates++;
		history += "#90L-1#";

		rotate90_r(5);
		cnt_rotates++;
		history += "#90R-5#";

		rotate90_r(1);
		cnt_rotates++;
		history += "#90R-1#";
	}
	else if (cube_colors[4][0][1] == 'b')
	{
		rotate90_r(5);
		cnt_rotates++;
		history += "#90R-5#";

		rotate90_l(3);
		cnt_rotates++;
		history += "#90L-3#";

		rotate180(5);
		cnt_rotates++;
		history += "#180-5#";

		rotate90_r(3);
		cnt_rotates++;
		history += "#90R-3#";

		rotate90_r(5);
		cnt_rotates++;
		history += "#90R-5#";

		rotate90_l(3);
		cnt_rotates++;
		history += "#90L-3#";

		rotate90_r(5);
		cnt_rotates++;
		history += "#90R-5#";

		rotate90_r(3);
		cnt_rotates++;
		history += "#90R-3#";

		rotate90_r(5);
		cnt_rotates++;
		history += "#90R-5#";

		rotate90_l(4);
		cnt_rotates++;
		history += "#90L-4#";

		rotate180(5);
		cnt_rotates++;
		history += "#180-5#";

		rotate90_r(4);
		cnt_rotates++;
		history += "#90R-4#";

		rotate90_r(5);
		cnt_rotates++;
		history += "#90R-5#";

		rotate90_l(4);
		cnt_rotates++;
		history += "#90L-4#";

		rotate90_r(5);
		cnt_rotates++;
		history += "#90R-5#";

		rotate90_r(4);
		cnt_rotates++;
		history += "#90R-4#";

		rotate90_r(5);
		cnt_rotates++;
		history += "#90R-5#";

		rotate90_l(3);
		cnt_rotates++;
		history += "#90L-3#";

		rotate180(5);
		cnt_rotates++;
		history += "#180-5#";

		rotate90_r(3);
		cnt_rotates++;
		history += "#90R-3#";

		rotate90_r(5);
		cnt_rotates++;
		history += "#90R-5#";

		rotate90_l(3);
		cnt_rotates++;
		history += "#90L-3#";

		rotate90_r(5);
		cnt_rotates++;
		history += "#90R-5#";

		rotate90_r(3);
		cnt_rotates++;
		history += "#90R-3#";
	}
	if (cube_colors[4][0][1] == 'y')
	{
		rotate90_r(5);
		cnt_rotates++;
		history += "#90R-5#";

		rotate90_l(2);
		cnt_rotates++;
		history += "#90L-2#";

		rotate180(5);
		cnt_rotates++;
		history += "#180-5#";

		rotate90_r(2);
		cnt_rotates++;
		history += "#90R-2#";

		rotate90_r(5);
		cnt_rotates++;
		history += "#90R-5#";

		rotate90_l(2);
		cnt_rotates++;
		history += "#90L-2#";

		rotate90_r(5);
		cnt_rotates++;
		history += "#90R-5#";

		rotate90_r(2);
		cnt_rotates++;
		history += "#90R-2#";
	}
}

void RCube::step_six(int& cnt_rotates, std::string& history)
{
	if ((cube_colors[4][0][2] == 'w' && cube_colors[1][0][0] == 'b' && cube_colors[5][2][0] == 'o') || (cube_colors[4][0][2] == 'o' && cube_colors[1][0][0] == 'w' && cube_colors[5][2][0] == 'b') || (cube_colors[4][0][2] == 'b' && cube_colors[1][0][0] == 'o' && cube_colors[5][2][0] == 'w'))
	{
		rotate90_l(1);
		cnt_rotates++;
		history += "#90L-1#";

		rotate90_l(4);
		cnt_rotates++;
		history += "#90L-4#";

		rotate90_r(1);
		cnt_rotates++;
		history += "#90R-1#";

		rotate90_l(2);
		cnt_rotates++;
		history += "#90L-2#";

		rotate90_l(1);
		cnt_rotates++;
		history += "#90L-1#";

		rotate90_r(4);
		cnt_rotates++;
		history += "#90R-4#";

		rotate90_r(1);
		cnt_rotates++;
		history += "#90R-1#";

		rotate90_r(2);
		cnt_rotates++;
		history += "#90R-2#";
	}
	else if ((cube_colors[2][0][2] == 'w' && cube_colors[3][0][0] == 'b' && cube_colors[5][0][2] == 'o') || (cube_colors[2][0][2] == 'o' && cube_colors[3][0][0] == 'w' && cube_colors[5][0][2] == 'b') || (cube_colors[2][0][2] == 'b' && cube_colors[3][0][0] == 'o' && cube_colors[5][0][2] == 'w'))
	{
		rotate90_l(2);
		cnt_rotates++;
		history += "#90L-2#";

		rotate90_l(1);
		cnt_rotates++;
		history += "#90L-1#";

		rotate90_l(4);
		cnt_rotates++;
		history += "#90L-4#";

		rotate90_r(1);
		cnt_rotates++;
		history += "#90R-1#";

		rotate90_r(2);
		cnt_rotates++;
		history += "#90R-2#";

		rotate90_l(1);
		cnt_rotates++;
		history += "#90L-1#";

		rotate90_r(4);
		cnt_rotates++;
		history += "#90R-4#";

		rotate90_r(1);
		cnt_rotates++;
		history += "#90R-1#";
	}
	else if ((cube_colors[3][0][2] == 'w' && cube_colors[4][0][0] == 'b' && cube_colors[5][0][0] == 'o') || (cube_colors[3][0][2] == 'o' && cube_colors[4][0][0] == 'w' && cube_colors[5][0][0] == 'b') || (cube_colors[3][0][2] == 'b' && cube_colors[4][0][0] == 'o' && cube_colors[5][0][0] == 'w'))
	{
		rotate90_l(1);
		cnt_rotates++;
		history += "#90L-1#";

		rotate90_l(4);
		cnt_rotates++;
		history += "#90L-4#";

		rotate90_l(3);
		cnt_rotates++;
		history += "#90L-3#";

		rotate90_r(4);
		cnt_rotates++;
		history += "#90R-4#";

		rotate90_r(1);
		cnt_rotates++;
		history += "#90R-1#";

		rotate90_l(4);
		cnt_rotates++;
		history += "#90L-4#";

		rotate90_r(3);
		cnt_rotates++;
		history += "#90R-3#";

		rotate90_r(4);
		cnt_rotates++;
		history += "#90R-4#";
	}
	if ((cube_colors[4][0][2] == 'b' && cube_colors[1][0][0] == 'y' && cube_colors[5][2][0] == 'o') || (cube_colors[4][0][2] == 'o' && cube_colors[1][0][0] == 'b' && cube_colors[5][2][0] == 'y') || (cube_colors[4][0][2] == 'y' && cube_colors[1][0][0] == 'o' && cube_colors[5][2][0] == 'b'))
	{
		rotate90_l(3);
		cnt_rotates++;
		history += "#90L-3#";

		rotate90_l(2);
		cnt_rotates++;
		history += "#90L-2#";

		rotate90_r(3);
		cnt_rotates++;
		history += "#90R-3#";

		rotate90_l(4);
		cnt_rotates++;
		history += "#90L-4#";

		rotate90_l(3);
		cnt_rotates++;
		history += "#90L-3#";

		rotate90_r(2);
		cnt_rotates++;
		history += "#90R-2#";

		rotate90_r(3);
		cnt_rotates++;
		history += "#90R-3#";

		rotate90_r(4);
		cnt_rotates++;
		history += "#90R-4#";
	}
	else if ((cube_colors[3][0][2] == 'b' && cube_colors[4][0][0] == 'y' && cube_colors[5][0][0] == 'o') || (cube_colors[3][0][2] == 'o' && cube_colors[4][0][0] == 'b' && cube_colors[5][0][0] == 'y') || ((cube_colors[3][0][2] == 'y' && cube_colors[4][0][0] == 'o' && cube_colors[5][0][0] == 'b')))
	{
		rotate90_l(4);
		cnt_rotates++;
		history += "#90L-4#";

		rotate90_l(3);
		cnt_rotates++;
		history += "#90L-3#";

		rotate90_l(2);
		cnt_rotates++;
		history += "#90L-2#";

		rotate90_r(3);
		cnt_rotates++;
		history += "#90R-3#";

		rotate90_r(4);
		cnt_rotates++;
		history += "#90R-4#";

		rotate90_l(3);
		cnt_rotates++;
		history += "#90L-3#";

		rotate90_r(2);
		cnt_rotates++;
		history += "#90R-2#";

		rotate90_r(3);
		cnt_rotates++;
		history += "#90R-3#";
	}
}

void RCube::step_seven(int& cnt_rotates, std::string& history)
{
	bool FirstRotation = true;
	int tries_cnt = 0;
	while (!IsSolved())
	{
		tries_cnt++;
		if (cube_colors[2][0][0] == 'o')
		{
			rotate90_l(1);
			cnt_rotates++;
			history += "#90L-1#";

			rotate90_r(2);
			cnt_rotates++;
			history += "#90R-2#";

			rotate90_r(1);
			cnt_rotates++;
			history += "#90R-1#";

			rotate90_l(2);
			cnt_rotates++;
			history += "#90L-2#";

			rotate90_l(1);
			cnt_rotates++;
			history += "#90L-1#";

			rotate90_r(2);
			cnt_rotates++;
			history += "#90R-2#";

			rotate90_r(1);
			cnt_rotates++;
			history += "#90R-1#";

			rotate90_l(2);
			cnt_rotates++;
			history += "#90L-2#";
			if (!FirstRotation)
			{
				rotate90_r(5);
				cnt_rotates++;
				history += "#90R-5#";
			}
		}
		else if (cube_colors[1][0][2] == 'o')
		{
			rotate90_r(2);
			cnt_rotates++;
			history += "#90R-2#";

			rotate90_l(1);
			cnt_rotates++;
			history += "#90L-1#";

			rotate90_l(2);
			cnt_rotates++;
			history += "#90L-2#";

			rotate90_r(1);
			cnt_rotates++;
			history += "#90R-1#";

			rotate90_r(2);
			cnt_rotates++;
			history += "#90R-2#";

			rotate90_l(1);
			cnt_rotates++;
			history += "#90L-1#";

			rotate90_l(2);
			cnt_rotates++;
			history += "#90L-2#";

			rotate90_r(1);
			cnt_rotates++;
			history += "#90R-1#";
			if (!FirstRotation)
			{
				rotate90_r(5);
				cnt_rotates++;
				history += "#90R-5#";
			}
		}
		else
		{
			rotate90_r(5);
			cnt_rotates++;
			history += "#90R-5#";
		}
		FirstRotation = false;
		if (cnt_rotates > 30000) // проверка на корректность кубика, если сделали больше 30к поворотов, то его 100% невозможно собрать!
		{
			history = "ERROR!";
			cnt_rotates = -1;
			return;
		}
	}
}