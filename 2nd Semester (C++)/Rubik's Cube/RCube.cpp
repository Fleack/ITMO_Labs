#include "RCube.h"

void RCube::scramble(void)
{
	srand((unsigned int)time(0));
	for (int i = 0; i < 20; i++)
	{
		int rand_face = rand()%6;
		if (rand() % 2 == 0)
			rotate90_l(rand_face);
		else
			rotate90_r(rand_face);
	}
}

bool RCube::IsCorrect(void)
{
	int colors_amount[6] = { 0,0,0,0,0,0 };
	for (int i = 0; i < 6; i++)
	{
		for (int j = 0; j < 3; j++)
		{
			for (int k = 0; k < 3; k++)
			{
				if (cube_colors[i][j][k] == 'r')
					colors_amount[0]++;
				else if (cube_colors[i][j][k] == 'w')
					colors_amount[1]++;
				else if (cube_colors[i][j][k] == 'b')
					colors_amount[2]++;
				else if (cube_colors[i][j][k] == 'y')
					colors_amount[3]++;
				else if (cube_colors[i][j][k] == 'g')
					colors_amount[4]++;
				else if (cube_colors[i][j][k] == 'o')
					colors_amount[5]++;
			}
		}
	}
	for (int i = 0; i < 6; i++)
	{
		if (colors_amount[i] != 9)
			return false;
	}
	solve();
	if (history == "ERROR!")
	{
		return false;
	}
	Solved = true;
	return true;
}

bool RCube::IsSolved(void)
{
	for (int i = 0; i < 6; i++)
	{
		char cur_face_color = cube_colors[i][0][0];
		for (int j = 0; j < 3; j++)
		{
			for (int k = 0; k < 3; k++)
			{
				if (cube_colors[i][j][k] != cur_face_color)
					return false;
			}
		}
	}
	return true;
}

RCube::RCube(const char(&other)[6][3][3]) : history(""), cnt_rotates(0), Solved(false)
{
	for (int i = 0; i < 6; i++)
	{
		for (int j = 0; j < 3; j++)
		{
			for (int k = 0; k < 3; k++)
			{
				cube_colors[i][j][k] = other[i][j][k];
			}
		}
	}
}

RCube::RCube() : history(""), cnt_rotates(0), Solved(false)
{
	std::vector<char>all_colors = { 'r', 'w', 'b', 'y', 'g', 'o'};
	for (int i = 0; i < 6; i++)
	{
		for (int j = 0; j < 3; j++)
		{
			for (int k = 0; k < 3; k++)
			{
				cube_colors[i][j][k] = all_colors[i];
			}
		}
	}
}

RCube::RCube(std::ifstream& input_file) : history(""), cnt_rotates(0), Solved(false)
{
	if (!input_file.is_open())
	{
		throw("File is not opened!");
	}
	for (int i = 0; i < 6; i++)
	{
		for (int j = 0; j < 3; j++)
		{
			std::string temp;
			input_file >> temp;
			for (int k = 0; k < 3; k++)
			{
				cube_colors[i][j][k] = temp[k];
			}
		}
	}
}

void RCube::save_cur_condition(std::ofstream& output_file)
{
	if (!output_file.is_open())
	{
		throw("File is not opened!");
	}
	for (int i = 0; i < 6; i++)
	{
		for (int j = 0; j < 3; j++)
		{
			for (int k = 0; k < 3; k++)
			{
				output_file << cube_colors[i][j][k];
			}
			output_file << '\n';
		}
		output_file << '\n';
	}
}

void RCube::file_input(std::ifstream& input_file)
{
	if (!input_file.is_open())
	{
		throw("File is not opened!");
	}
	for (int i = 0; i < 6; i++)
	{
		for (int j = 0; j < 3; j++)
		{
			std::string temp;
			input_file >> temp;
			for (int k = 0; k < 3; k++)
			{
				cube_colors[i][j][k] = temp[k];
			}
		}
	}
}

void RCube::print_cur_condition()
{
	for (int i = 0; i < 6; i++)
	{
		for (int j = 0; j < 3; j++)
		{
			for (int k = 0; k < 3; k++)
			{
				std::cout << cube_colors[i][j][k];
			}
			std::cout << '\n';
		}
		std::cout << '\n';
	}
}

void RCube::rotate90_r(int face_id)
{

	char temp, temp2, temp3, temp4;

	// поворот угловых квадратиков
	temp = cube_colors[face_id][0][0];
	cube_colors[face_id][0][0] = cube_colors[face_id][2][0];

	temp2 = cube_colors[face_id][0][2];
	cube_colors[face_id][0][2] = temp;

	temp = cube_colors[face_id][2][2];
	cube_colors[face_id][2][2] = temp2;

	cube_colors[face_id][2][0] = temp;

	// поворот центральных квадратиков
	temp = cube_colors[face_id][0][1];
	cube_colors[face_id][0][1] = cube_colors[face_id][1][0];

	temp2 = cube_colors[face_id][1][2];
	cube_colors[face_id][1][2] = temp;

	temp = cube_colors[face_id][2][1];
	cube_colors[face_id][2][1] = temp2;

	cube_colors[face_id][1][0] = temp;

	// поворот соседних к выбранной граней 
	char temp_arr[4][3];
	switch (face_id) // доделать свитч
	{
	case 0:
		// сохраняем квадратики боковых граней, прилегающиx к выбранной
		temp_arr[0][0] = cube_colors[1][2][0]; temp_arr[0][1] = cube_colors[1][2][1]; temp_arr[0][2] = cube_colors[1][2][2];
		temp_arr[1][0] = cube_colors[2][2][0]; temp_arr[1][1] = cube_colors[2][2][1]; temp_arr[1][2] = cube_colors[2][2][2];
		temp_arr[2][0] = cube_colors[3][2][0]; temp_arr[2][1] = cube_colors[3][2][1]; temp_arr[2][2] = cube_colors[3][2][2];
		temp_arr[3][0] = cube_colors[4][2][0]; temp_arr[3][1] = cube_colors[4][2][1]; temp_arr[3][2] = cube_colors[4][2][2];

		// двигаем на 1
		cube_colors[1][2][0] = temp_arr[3][0]; cube_colors[1][2][1] = temp_arr[3][1]; cube_colors[1][2][2] = temp_arr[3][2];
		cube_colors[2][2][0] = temp_arr[0][0]; cube_colors[2][2][1] = temp_arr[0][1]; cube_colors[2][2][2] = temp_arr[0][2];
		cube_colors[3][2][0] = temp_arr[1][0]; cube_colors[3][2][1] = temp_arr[1][1]; cube_colors[3][2][2] = temp_arr[1][2];
		cube_colors[4][2][0] = temp_arr[2][0]; cube_colors[4][2][1] = temp_arr[2][1]; cube_colors[4][2][2] = temp_arr[2][2];
		break;
	case 1: 
		// сохраняем квадратики боковых граней, прилегающиx к выбранной
		temp_arr[0][0] = cube_colors[5][2][0]; temp_arr[0][1] = cube_colors[5][2][1]; temp_arr[0][2] = cube_colors[5][2][2];
		temp_arr[1][0] = cube_colors[2][0][0]; temp_arr[1][1] = cube_colors[2][1][0]; temp_arr[1][2] = cube_colors[2][2][0];
		temp_arr[2][0] = cube_colors[0][0][0]; temp_arr[2][1] = cube_colors[0][0][1]; temp_arr[2][2] = cube_colors[0][0][2];
		temp_arr[3][0] = cube_colors[4][2][2]; temp_arr[3][1] = cube_colors[4][1][2]; temp_arr[3][2] = cube_colors[4][0][2];

		// двигаем на 1
		cube_colors[5][2][0] = temp_arr[3][0]; cube_colors[5][2][1] = temp_arr[3][1]; cube_colors[5][2][2] = temp_arr[3][2];
		cube_colors[2][0][0] = temp_arr[0][0]; cube_colors[2][1][0] = temp_arr[0][1]; cube_colors[2][2][0] = temp_arr[0][2];
		cube_colors[0][0][2] = temp_arr[1][0]; cube_colors[0][0][1] = temp_arr[1][1]; cube_colors[0][0][0] = temp_arr[1][2];
		cube_colors[4][0][2] = temp_arr[2][0]; cube_colors[4][1][2] = temp_arr[2][1]; cube_colors[4][2][2] = temp_arr[2][2];
		break;
	case 2:
		// сохраняем квадратики боковых граней, прилегающиx к выбранной
		temp_arr[0][0] = cube_colors[5][2][2]; temp_arr[0][1] = cube_colors[5][1][2]; temp_arr[0][2] = cube_colors[5][0][2];
		temp_arr[1][0] = cube_colors[3][0][0]; temp_arr[1][1] = cube_colors[3][1][0]; temp_arr[1][2] = cube_colors[3][2][0];
		temp_arr[2][0] = cube_colors[0][2][2]; temp_arr[2][1] = cube_colors[0][1][2]; temp_arr[2][2] = cube_colors[0][0][2];
		temp_arr[3][0] = cube_colors[1][2][2]; temp_arr[3][1] = cube_colors[1][1][2]; temp_arr[3][2] = cube_colors[1][0][2];

		// двигаем на 1
		cube_colors[5][2][2] = temp_arr[3][0]; cube_colors[5][1][2] = temp_arr[3][1]; cube_colors[5][0][2] = temp_arr[3][2];
		cube_colors[3][0][0] = temp_arr[0][0]; cube_colors[3][1][0] = temp_arr[0][1]; cube_colors[3][2][0] = temp_arr[0][2];
		cube_colors[0][0][2] = temp_arr[1][2]; cube_colors[0][1][2] = temp_arr[1][1]; cube_colors[0][2][2] = temp_arr[1][0];
		cube_colors[1][2][2] = temp_arr[2][0]; cube_colors[1][1][2] = temp_arr[2][1]; cube_colors[1][0][2] = temp_arr[2][2];
		break;
	case 3:
		// сохраняем квадратики боковых граней, прилегающиx к выбранной
		temp_arr[0][0] = cube_colors[5][0][2]; temp_arr[0][1] = cube_colors[5][0][1]; temp_arr[0][2] = cube_colors[5][0][0];
		temp_arr[1][0] = cube_colors[4][0][0]; temp_arr[1][1] = cube_colors[4][1][0]; temp_arr[1][2] = cube_colors[4][2][0];
		temp_arr[2][0] = cube_colors[0][2][0]; temp_arr[2][1] = cube_colors[0][2][1]; temp_arr[2][2] = cube_colors[0][2][2];
		temp_arr[3][0] = cube_colors[2][2][2]; temp_arr[3][1] = cube_colors[2][1][2]; temp_arr[3][2] = cube_colors[2][0][2];

		// двигаем на 1
		cube_colors[5][0][0] = temp_arr[3][2]; cube_colors[5][0][1] = temp_arr[3][1]; cube_colors[5][0][2] = temp_arr[3][0];
		cube_colors[4][0][0] = temp_arr[0][0]; cube_colors[4][1][0] = temp_arr[0][1]; cube_colors[4][2][0] = temp_arr[0][2];
		cube_colors[0][2][2] = temp_arr[1][2]; cube_colors[0][2][1] = temp_arr[1][1]; cube_colors[0][2][0] = temp_arr[1][0];
		cube_colors[2][2][2] = temp_arr[2][0]; cube_colors[2][1][2] = temp_arr[2][1]; cube_colors[2][0][2] = temp_arr[2][2];
		break;
	case 4:
		// сохраняем квадратики боковых граней, прилегающиx к выбранной
		temp_arr[0][0] = cube_colors[5][0][0]; temp_arr[0][1] = cube_colors[5][1][0]; temp_arr[0][2] = cube_colors[5][2][0];
		temp_arr[1][0] = cube_colors[1][0][0]; temp_arr[1][1] = cube_colors[1][1][0]; temp_arr[1][2] = cube_colors[1][2][0];
		temp_arr[2][0] = cube_colors[0][0][0]; temp_arr[2][1] = cube_colors[0][1][0]; temp_arr[2][2] = cube_colors[0][2][0];
		temp_arr[3][0] = cube_colors[3][2][2]; temp_arr[3][1] = cube_colors[3][1][2]; temp_arr[3][2] = cube_colors[3][0][2];

		// двигаем на 1
		cube_colors[5][0][0] = temp_arr[3][0]; cube_colors[5][1][0] = temp_arr[3][1]; cube_colors[5][2][0] = temp_arr[3][2];
		cube_colors[1][0][0] = temp_arr[0][0]; cube_colors[1][1][0] = temp_arr[0][1]; cube_colors[1][2][0] = temp_arr[0][2];
		cube_colors[0][0][0] = temp_arr[1][0]; cube_colors[0][1][0] = temp_arr[1][1]; cube_colors[0][2][0] = temp_arr[1][2];
		cube_colors[3][2][2] = temp_arr[2][0]; cube_colors[3][1][2] = temp_arr[2][1]; cube_colors[3][0][2] = temp_arr[2][2];
		break;
	case 5:
		// сохраняем квадратики боковых граней, прилегающиx к выбранной
		temp_arr[0][0] = cube_colors[1][0][0]; temp_arr[0][1] = cube_colors[1][0][1]; temp_arr[0][2] = cube_colors[1][0][2];
		temp_arr[1][0] = cube_colors[2][0][0]; temp_arr[1][1] = cube_colors[2][0][1]; temp_arr[1][2] = cube_colors[2][0][2];
		temp_arr[2][0] = cube_colors[3][0][0]; temp_arr[2][1] = cube_colors[3][0][1]; temp_arr[2][2] = cube_colors[3][0][2];
		temp_arr[3][0] = cube_colors[4][0][0]; temp_arr[3][1] = cube_colors[4][0][1]; temp_arr[3][2] = cube_colors[4][0][2];

		// двигаем на 1
		cube_colors[1][0][0] = temp_arr[1][0]; cube_colors[1][0][1] = temp_arr[1][1]; cube_colors[1][0][2] = temp_arr[1][2];
		cube_colors[2][0][0] = temp_arr[2][0]; cube_colors[2][0][1] = temp_arr[2][1]; cube_colors[2][0][2] = temp_arr[2][2];
		cube_colors[3][0][0] = temp_arr[3][0]; cube_colors[3][0][1] = temp_arr[3][1]; cube_colors[3][0][2] = temp_arr[3][2];
		cube_colors[4][0][0] = temp_arr[0][0]; cube_colors[4][0][1] = temp_arr[0][1]; cube_colors[4][0][2] = temp_arr[0][2];
		break;
	default:
		throw("Wrong face");
		break;
	}
}

void RCube::rotate90_l(int face_id)
{
	char temp, temp2, temp3, temp4;

	// поворот угловых квадратиков
	temp = cube_colors[face_id][0][0];
	cube_colors[face_id][0][0] = cube_colors[face_id][0][2];

	temp2 = cube_colors[face_id][2][0];
	cube_colors[face_id][2][0] = temp;

	temp = cube_colors[face_id][2][2];
	cube_colors[face_id][2][2] = temp2;

	cube_colors[face_id][0][2] = temp;

	// поворот центральных квадратиков
	temp = cube_colors[face_id][0][1];
	cube_colors[face_id][0][1] = cube_colors[face_id][1][2];

	temp2 = cube_colors[face_id][1][0];
	cube_colors[face_id][1][0] = temp;

	temp = cube_colors[face_id][2][1];
	cube_colors[face_id][2][1] = temp2;

	cube_colors[face_id][1][2] = temp;

	// поворот соседних к выбранной граней 
	char temp_arr[4][3];
	switch (face_id) // доделать свитч
	{
	case 0:
		// сохраняем квадратики боковых граней, прилегающиx к выбранной
		temp_arr[0][0] = cube_colors[1][2][0]; temp_arr[0][1] = cube_colors[1][2][1]; temp_arr[0][2] = cube_colors[1][2][2];
		temp_arr[1][0] = cube_colors[2][2][0]; temp_arr[1][1] = cube_colors[2][2][1]; temp_arr[1][2] = cube_colors[2][2][2];
		temp_arr[2][0] = cube_colors[3][2][0]; temp_arr[2][1] = cube_colors[3][2][1]; temp_arr[2][2] = cube_colors[3][2][2];
		temp_arr[3][0] = cube_colors[4][2][0]; temp_arr[3][1] = cube_colors[4][2][1]; temp_arr[3][2] = cube_colors[4][2][2];

		// двигаем на 1
		cube_colors[1][2][0] = temp_arr[1][0]; cube_colors[1][2][1] = temp_arr[1][1]; cube_colors[1][2][2] = temp_arr[1][2];
		cube_colors[2][2][0] = temp_arr[2][0]; cube_colors[2][2][1] = temp_arr[2][1]; cube_colors[2][2][2] = temp_arr[2][2];
		cube_colors[3][2][0] = temp_arr[3][0]; cube_colors[3][2][1] = temp_arr[3][1]; cube_colors[3][2][2] = temp_arr[3][2];
		cube_colors[4][2][0] = temp_arr[0][0]; cube_colors[4][2][1] = temp_arr[0][1]; cube_colors[4][2][2] = temp_arr[0][2];
		break;
	case 1:
		// сохраняем квадратики боковых граней, прилегающиx к выбранной
		temp_arr[0][0] = cube_colors[5][2][0]; temp_arr[0][1] = cube_colors[5][2][1]; temp_arr[0][2] = cube_colors[5][2][2];
		temp_arr[1][0] = cube_colors[2][0][0]; temp_arr[1][1] = cube_colors[2][1][0]; temp_arr[1][2] = cube_colors[2][2][0];
		temp_arr[2][0] = cube_colors[0][0][0]; temp_arr[2][1] = cube_colors[0][0][1]; temp_arr[2][2] = cube_colors[0][0][2];
		temp_arr[3][0] = cube_colors[4][2][2]; temp_arr[3][1] = cube_colors[4][1][2]; temp_arr[3][2] = cube_colors[4][0][2];

		// двигаем на 1
		cube_colors[5][2][0] = temp_arr[1][0]; cube_colors[5][2][1] = temp_arr[1][1]; cube_colors[5][2][2] = temp_arr[1][2];
		cube_colors[2][0][0] = temp_arr[2][2]; cube_colors[2][1][0] = temp_arr[2][1]; cube_colors[2][2][0] = temp_arr[2][0];
		cube_colors[0][0][2] = temp_arr[3][0]; cube_colors[0][0][1] = temp_arr[3][1]; cube_colors[0][0][0] = temp_arr[3][2];
		cube_colors[4][0][2] = temp_arr[0][2]; cube_colors[4][1][2] = temp_arr[0][1]; cube_colors[4][2][2] = temp_arr[0][0];
		break;
	case 2:
		// сохраняем квадратики боковых граней, прилегающиx к выбранной
		temp_arr[0][0] = cube_colors[5][2][2]; temp_arr[0][1] = cube_colors[5][1][2]; temp_arr[0][2] = cube_colors[5][0][2];
		temp_arr[1][0] = cube_colors[3][0][0]; temp_arr[1][1] = cube_colors[3][1][0]; temp_arr[1][2] = cube_colors[3][2][0];
		temp_arr[2][0] = cube_colors[0][2][2]; temp_arr[2][1] = cube_colors[0][1][2]; temp_arr[2][2] = cube_colors[0][0][2];
		temp_arr[3][0] = cube_colors[1][2][2]; temp_arr[3][1] = cube_colors[1][1][2]; temp_arr[3][2] = cube_colors[1][0][2];

		// двигаем на 1
		cube_colors[5][2][2] = temp_arr[1][0]; cube_colors[5][1][2] = temp_arr[1][1]; cube_colors[5][0][2] = temp_arr[1][2];
		cube_colors[3][0][0] = temp_arr[2][0]; cube_colors[3][1][0] = temp_arr[2][1]; cube_colors[3][2][0] = temp_arr[2][2];
		cube_colors[0][0][2] = temp_arr[3][2]; cube_colors[0][1][2] = temp_arr[3][1]; cube_colors[0][2][2] = temp_arr[3][0];
		cube_colors[1][2][2] = temp_arr[0][0]; cube_colors[1][1][2] = temp_arr[0][1]; cube_colors[1][0][2] = temp_arr[0][2];
		break;
	case 3:
		// сохраняем квадратики боковых граней, прилегающиx к выбранной
		temp_arr[0][0] = cube_colors[5][0][2]; temp_arr[0][1] = cube_colors[5][0][1]; temp_arr[0][2] = cube_colors[5][0][0];
		temp_arr[1][0] = cube_colors[4][0][0]; temp_arr[1][1] = cube_colors[4][1][0]; temp_arr[1][2] = cube_colors[4][2][0];
		temp_arr[2][0] = cube_colors[0][2][0]; temp_arr[2][1] = cube_colors[0][2][1]; temp_arr[2][2] = cube_colors[0][2][2];
		temp_arr[3][0] = cube_colors[2][2][2]; temp_arr[3][1] = cube_colors[2][1][2]; temp_arr[3][2] = cube_colors[2][0][2];

		// двигаем на 1
		cube_colors[5][0][0] = temp_arr[1][2]; cube_colors[5][0][1] = temp_arr[1][1]; cube_colors[5][0][2] = temp_arr[1][0];
		cube_colors[4][0][0] = temp_arr[2][0]; cube_colors[4][1][0] = temp_arr[2][1]; cube_colors[4][2][0] = temp_arr[2][2];
		cube_colors[0][2][2] = temp_arr[3][2]; cube_colors[0][2][1] = temp_arr[3][1]; cube_colors[0][2][0] = temp_arr[3][0];
		cube_colors[2][2][2] = temp_arr[0][0]; cube_colors[2][1][2] = temp_arr[0][1]; cube_colors[2][0][2] = temp_arr[0][2];
		break;
	case 4:
		// сохраняем квадратики боковых граней, прилегающиx к выбранной
		temp_arr[0][0] = cube_colors[5][0][0]; temp_arr[0][1] = cube_colors[5][1][0]; temp_arr[0][2] = cube_colors[5][2][0];
		temp_arr[1][0] = cube_colors[1][0][0]; temp_arr[1][1] = cube_colors[1][1][0]; temp_arr[1][2] = cube_colors[1][2][0];
		temp_arr[2][0] = cube_colors[0][0][0]; temp_arr[2][1] = cube_colors[0][1][0]; temp_arr[2][2] = cube_colors[0][2][0];
		temp_arr[3][0] = cube_colors[3][2][2]; temp_arr[3][1] = cube_colors[3][1][2]; temp_arr[3][2] = cube_colors[3][0][2];

		// двигаем на 1
		cube_colors[5][0][0] = temp_arr[1][0]; cube_colors[5][1][0] = temp_arr[1][1]; cube_colors[5][2][0] = temp_arr[1][2];
		cube_colors[1][0][0] = temp_arr[2][0]; cube_colors[1][1][0] = temp_arr[2][1]; cube_colors[1][2][0] = temp_arr[2][2];
		cube_colors[0][0][0] = temp_arr[3][0]; cube_colors[0][1][0] = temp_arr[3][1]; cube_colors[0][2][0] = temp_arr[3][2];
		cube_colors[3][2][2] = temp_arr[0][0]; cube_colors[3][1][2] = temp_arr[0][1]; cube_colors[3][0][2] = temp_arr[0][2];
		break;
	case 5:
		// сохраняем квадратики боковых граней, прилегающиx к выбранной
		temp_arr[0][0] = cube_colors[1][0][0]; temp_arr[0][1] = cube_colors[1][0][1]; temp_arr[0][2] = cube_colors[1][0][2];
		temp_arr[1][0] = cube_colors[2][0][0]; temp_arr[1][1] = cube_colors[2][0][1]; temp_arr[1][2] = cube_colors[2][0][2];
		temp_arr[2][0] = cube_colors[3][0][0]; temp_arr[2][1] = cube_colors[3][0][1]; temp_arr[2][2] = cube_colors[3][0][2];
		temp_arr[3][0] = cube_colors[4][0][0]; temp_arr[3][1] = cube_colors[4][0][1]; temp_arr[3][2] = cube_colors[4][0][2];

		// двигаем на 1
		cube_colors[1][0][0] = temp_arr[3][0]; cube_colors[1][0][1] = temp_arr[3][1]; cube_colors[1][0][2] = temp_arr[3][2];
		cube_colors[2][0][0] = temp_arr[0][0]; cube_colors[2][0][1] = temp_arr[0][1]; cube_colors[2][0][2] = temp_arr[0][2];
		cube_colors[3][0][0] = temp_arr[1][0]; cube_colors[3][0][1] = temp_arr[1][1]; cube_colors[3][0][2] = temp_arr[1][2];
		cube_colors[4][0][0] = temp_arr[2][0]; cube_colors[4][0][1] = temp_arr[2][1]; cube_colors[4][0][2] = temp_arr[2][2];
		break;
	default:
		throw("Wrong face");
		break;
	}
}

void RCube::rotate180(int face_id)
{
	rotate90_r(face_id);
	rotate90_r(face_id);
}

void RCube::print_solution()
{
	int hashtags_cnt = 0;
	for (int i = 0; i < history.size(); i++)
	{
		if (history[i] == '#')
		{
			hashtags_cnt++;
		}
		else
		{
			std::cout << history[i];
		}
		if (hashtags_cnt%2 == 0)
		{
			std::cout << '\n';
		}
	}
	std::cout << "\n\nВсего поворотов граней было совершено: " << cnt_rotates;
}



// [6] - количество граней, [3][3] - квадратик выбранной грани.
// Описание граний:
// 0 - нижняя, 1 - передняя
// 2 - правая, 3 - задняя
// 4 - левая, 5 - верхняя
