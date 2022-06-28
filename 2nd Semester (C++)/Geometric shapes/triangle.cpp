#include<iostream>
#include<vector>
#include<tgmath.h>
#include<algorithm>
#pragma once
# define PI           3.14159265358979323846

class triangle
{
public:

	triangle(const double x1_set, const double y1_set, const double x2_set, const double y2_set, const  double x3_set, const double y3_set)
	{
		set_coordinates(x1_set, y1_set, x2_set, y2_set, x3_set, y3_set);
		set_side_vectors();
	}

	triangle()
	{
		set_coordinates(0, 0, 0, 0, 0, 0);
		set_side_vectors();
	}

	triangle(const triangle& other): x1(other.x1), y1(other.y1), x2(other.x2), y2(other.y2), x3(other.x3), y3(other.y3)
	{
	}

	triangle& operator=(const triangle& other)
	{
		this->x1 = other.x1;
		this->y1 = other.y1;
		this->x2 = other.x2;
		this->y2 = other.y2;
		this->x3 = other.x3;
		this->y3 = other.y3;
	}

	void set_coordinates(const double x1_set, const double y1_set, const double x2_set, const double y2_set, const double x3_set, const double y3_set)
	{
		x1 = x1_set;
		y1 = y1_set;
		x2 = x2_set;
		y2 = y2_set;
		x3 = x3_set;
		y3 = y3_set;
	}

	double get_square()
	{
		square = abs(((x2-x1)*(y3-y1)-(x3-x1)*(y2-y1))/2);
		return square;
	}

	double get_perimeter()
	{
		side_a1_len = get_side_len(x1, y1, x2, y2);
		side_a2_len = get_side_len(x1, y1, x3, y3);
		side_a3_len = get_side_len(x2, y2, x3, y3);
		perimeter = side_a1_len + side_a2_len + side_a3_len;
		return perimeter;
	}

	double get_angle(const int num)
	{
		double cos_angle;
		double scalar;
		if (num == 1)
		{
			scalar = abs(v_a1[0] * v_a2[0] + v_a1[1] * v_a2[1]);
			cos_angle = scalar / (side_a1_len * side_a2_len);
			angle_12 = (double)(180 / PI *acos(cos_angle));
		}
		else if (num == 2)
		{
			scalar = abs(v_a1[0] * v_a3[0] + v_a1[1] * v_a3[1]);
			cos_angle = scalar / (side_a1_len * side_a3_len);
			angle_13 = (double)(180 / PI * acos(cos_angle));
		}
		else if (num == 3)
		{
			 scalar = abs(v_a3[0] * v_a2[0] + v_a3[1] * v_a2[1]);
			 cos_angle = scalar / (side_a3_len * side_a2_len);
			 angle_23 = (double)(180 / PI * acos(cos_angle));
		}
		else
		{
			throw "bruh man";
		}
		return (double)(180 / PI *acos(cos_angle));
	}

private:
	void set_side_vectors()
	{
		v_a1 = { x2 - x1, y2 - y1 };
		v_a2 = { x3 - x1, y3 - y1 };
		v_a3 = { x2 - x3, y2 - y3 };
	}
	double get_side_len(const double x1, const double y1, const double x2, const double y2)
	{
		double side_len = sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1));
		return side_len;
	}
	std::vector<double>v_a1;
	std::vector<double>v_a2;
	std::vector<double>v_a3;
	double side_a1_len = 0;
	double side_a2_len = 0;
	double side_a3_len = 0;
	double square = 0;
	double perimeter = 0;
	double x1 = 0;
	double y1 = 0;
	double x2 = 0;
	double y2 = 0;
	double x3 = 0;
	double y3 = 0;
	double angle_12 = 0;
	double angle_13 = 0;
	double angle_23 = 0;
};