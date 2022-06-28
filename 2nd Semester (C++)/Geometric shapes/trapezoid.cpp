#include<iostream>
#include<vector>
#include<tgmath.h>
#include<algorithm>
#pragma once

class trapezoid
{
public:
	trapezoid(const double x1_set, const double y1_set, const double x2_set, const double y2_set, const double x3_set, const double y3_set, const double x4_set, const double y4_set)
	{
		is_trapezoid( x1_set,  y1_set,  x2_set,  y2_set,  x3_set,  y3_set,  x4_set,  y4_set);
	}

	trapezoid(const trapezoid& other) : x1(other.x1), y1(other.y1), x2(other.x2), y2(other.y2), x3(other.x3), y3(other.y3), x4(other.x4), y4(other.y4)
	{
	}

	trapezoid& operator=(const trapezoid& other)
	{
		this->x1 = x1;
		this->y1 = y1;
		this->x2 = x2;
		this->y2 = y2;
		this->x3 = x3;
		this->y3 = y3;
		this->x4 = x4;
		this->y4 = y4;
	}

	double get_perimeter()
	{
		double perimeter = 0;
		double a1 = 0; double a2 = 0; double a3 = 0; double a4 = 0;
		if (id_cnt == 1) // x1x2 parallel x3x4
		{
			a1 = get_side_len(x3, y3, x4, y4);
			a2 = get_side_len(x1, y1, x2, y2);
			//closes point to x3
			double dist_x3_x1 = get_dist_to_dot(x3, y3, x1, y1);
			double dist_x3_x2 = get_dist_to_dot(x3, y3, x2, y2);
			if (dist_x3_x1 < dist_x3_x2) // x1 is closest
			{
				a3 = get_side_len(x3, y3, x1, y1);
				a4 = get_side_len(x4, y4, x2, y2);
			}
			else // x2 is closest
			{
				a3 = get_side_len(x3, y3, x2, y2);
				a4 = get_side_len(x4, y4, x1, y1);
			}
		}
		else if (id_cnt == 2) // x1x4 parallel x2x3
		{
			a1 = get_side_len(x1, y1, x4, y4);
			a2 = get_side_len(x3, y3, x2, y2);
			//closes point to x3
			double dist_x3_x1 = get_dist_to_dot(x3, y3, x1, y1);
			double dist_x3_x4 = get_dist_to_dot(x3, y3, x4, y4);
			if (dist_x3_x1 < dist_x3_x4) // x1 is closest
			{
				a3 = get_side_len(x3, y3, x1, y1);
				a4 = get_side_len(x4, y4, x2, y2);
			}
			else // x4 is closest
			{
				a3 = get_side_len(x3, y3, x4, y4);
				a4 = get_side_len(x2, y2, x1, y1);
			}
		}
		else if (id_cnt == 3) // x1x3 parallel x2x4
		{
			a1 = get_side_len(x1, y1, x3, y3);
			a2 = get_side_len(x4, y4, x2, y2);
			//closes point to x3
			double dist_x3_x2 = get_dist_to_dot(x3, y3, x2, y2);
			double dist_x3_x4 = get_dist_to_dot(x3, y3, x4, y4);
			if (dist_x3_x4 < dist_x3_x2) // x4 is closest
			{
				a3 = get_side_len(x3, y3, x4, y4);
				a4 = get_side_len(x1, y1, x2, y2);
			}
			else // x2 is closest
			{
				a3 = get_side_len(x3, y3, x2, y2);
				a4 = get_side_len(x4, y4, x1, y1);
			}
		}
		perimeter = a1 + a2 + a3 + a4;
		return perimeter;
	}

	double get_square()
	{
		double square;
		double a1 = 0; double a2 = 0;
		double perpendicular = -1;
		double mid = 1;
		if (id_cnt == 1) // x1x2 parallel x3x4
		{
			a1 = get_side_len(x3, y3, x4, y4);
			a2 = get_side_len(x1, y1, x2, y2);
			std::vector<double>x1x3 = { x3 - x1, y3 - y1, 0};
			std::vector<double>x1x2 = { x2 - x1, y2 - y1, 0};
			std::vector<double>v(3);
			v[0] = 0;
			v[1] = 0;
			v[2] = x1x3[0] * x1x2[1] - x1x3[1] * x1x2[0];
			double v_len = sqrt(v[2] * v[2]);
			double x1x2_len = sqrt(x1x2[0] * x1x2[0] + x1x2[1] * x1x2[1]);
			perpendicular = v_len / x1x2_len;
		}
		else if (id_cnt == 2) // x1x4 parallel x2x3
		{
			a1 = get_side_len(x1, y1, x4, y4);
			a2 = get_side_len(x3, y3, x2, y2);
			std::vector<double>x1x3 = { x3 - x1, y3 - y1, 0 };
			std::vector<double>x3x2 = { x2 - x3, y2 - y3, 0 };
			std::vector<double>v(3);
			v[0] = 0;
			v[1] = 0;
			v[2] = x1x3[0] * x3x2[1] - x1x3[1] * x3x2[0];
			double v_len = sqrt(v[2] * v[2]);
			double x1x2_len = sqrt(x3x2[0] * x3x2[0] + x3x2[1] * x3x2[1]);
			perpendicular = v_len / x1x2_len;
		}
		else if (id_cnt == 3) // x1x3 parallel x2x4
		{
			a1 = get_side_len(x1, y1, x3, y3);
			a2 = get_side_len(x4, y4, x2, y2);
			std::vector<double>x1x2 = { x2 - x1, y2 - y1, 0 };
			std::vector<double>x3x1 = { x1 - x3, y1 - y3, 0 };
			std::vector<double>v(3);
			v[0] = 0;
			v[1] = 0;
			v[2] = x1x2[0] * x3x1[1] - x1x2[1] * x3x1[0];
			double v_len = sqrt(v[2] * v[2]);
			double x1x2_len = sqrt(x3x1[0] * x3x1[0] + x3x1[1] * x3x1[1]);
			perpendicular = v_len / x1x2_len;
		}
		mid = (a1 + a2) / 2;
		square = perpendicular * mid;
		return square;
	}

private:

	double get_dist_to_dot(const double x1, const double y1, const double x2, const double y2) const
	{
		return sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1));
	}

	double get_side_len(const double x1, const double y1, const double x2, const double y2)
	{
		double side_len = sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1));
		return side_len;
	}

	void is_trapezoid(const double x1, const double y1, const double x2, const double y2, const  double x3, const double y3, const double x4, const double y4)
	{
		v_a1 = {x2-x1, y2-y1};
		v_a2 = {x4-x3, y4-y3};
		u_a1 = {x4-x1, y4-y1};
		u_a2 = {x3-x2, y3-y2};
		p_a1 = {x3-x1, y3-y1};
		p_a2 = {x4-x2, y4-y2};
		int parallels_cnt = 0;
		if (v_a1[0]/v_a2[0]==v_a1[1]/v_a2[1] || (v_a1[0] == 0 && v_a2[0] == 0) || (v_a1[1] == 0 && v_a2[1] == 0))
		{
			parallels_cnt++;
			id_cnt = 1;
		}
		if (u_a1[0] / u_a2[0] == u_a1[1] / u_a2[1] || (u_a1[0] == 0 && u_a2[0] == 0) || (u_a1[1] == 0 && u_a2[1] == 0))
		{
			parallels_cnt++;
			id_cnt = 2;
		}
		if (p_a1[0] / p_a2[0] == p_a1[1] / p_a2[1] || (p_a1[0] == 0 && p_a2[0] == 0) || (p_a1[1] == 0 && p_a2[1] == 0))
		{
			parallels_cnt++;
			id_cnt = 3;
		}
		if (parallels_cnt != 1)
		{
			throw "Not a trapezoid";
		}
	}

	std::vector<double>v_a1;
	std::vector<double>v_a2;
	std::vector<double>u_a1;
	std::vector<double>u_a2;
	std::vector<double>p_a1;
	std::vector<double>p_a2;
	double x1 = 0;
	double y1 = 0;
	double x2 = 0;
	double y2 = 0;
	double x3 = 0;
	double y3 = 0;
	double x4 = 0;
	double y4 = 0;
	double perimeter = 0;
	int id_cnt = -1; // parallel lines
};