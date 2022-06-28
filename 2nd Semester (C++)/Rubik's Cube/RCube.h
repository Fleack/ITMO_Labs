#include <fstream>
#include <ctime>
#include <iostream>
#include <vector>
#include <string>
#pragma once

class RCube
{
public:

	void scramble(void);

	bool IsCorrect(void);

	bool IsSolved(void);

	RCube(const char(&other)[6][3][3]);

	RCube();

	RCube(std::ifstream& input_file);

	void save_cur_condition(std::ofstream& output_file);

	void file_input(std::ifstream& input_file);

	void print_cur_condition();

	void rotate90_r(int face_id);

	void rotate90_l(int face_id);

	void rotate180(int face_id);

	void print_solution();

	void solve();

private:

	void step_one(int& cnt_rotates, std::string& history);

	void step_two(int& cnt_rotates, std::string& history);

	void step_three(int& cnt_rotates, std::string& history);

	void step_four(int& cnt_rotates, std::string& history);

	void step_five(int& cnt_rotates, std::string& history);

	void step_six(int& cnt_rotates, std::string& history);

	void step_seven(int& cnt_rotates, std::string& history);

	// [6] - количество граней, [3][3] - квадратик выбранной грани.
	// описание граний:
	// 0 - нижн¤¤, 1 - передн¤¤
	// 2 - права¤, 3 - задн¤¤
	// 4 - лева¤, 5 - верхн¤¤
	char cube_colors[6][3][3];
	std::string history;
	int cnt_rotates;
	bool Solved;
};

