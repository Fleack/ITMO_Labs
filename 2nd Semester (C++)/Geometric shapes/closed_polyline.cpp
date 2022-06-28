#include<iostream>
#include<vector>
#include<tgmath.h>
#include<algorithm>
#include"polyline.h"
#pragma once

class closed_polyline : polyline
{

public:
	closed_polyline(const std::vector<point>other)
	{
		this->points.resize(other.size());
		for (int i = 0; i < (int)other.size(); i++)
		{
			this->points[i] = other[i];
		}
		if (this->points[0] != this->points[this->points.size()-1] && this->points.size() > 2)
		{
			this->points.push_back(this->points[0]);
		}
	}

	closed_polyline()
	{
		this->points.resize(1);
		this->points[0] = { 0, 0 };
	}

	closed_polyline(const closed_polyline& other)
	{
		this->points.resize(other.points.size());
		for (int i = 0; i < (int)other.points.size(); i++)
		{
			this->points[i] = other.points[i];
		}
	}

	closed_polyline& operator=(const closed_polyline& other) override
	{
		this->points.resize(other.points.size());
		for (int i = 0; i < (int)other.points.size(); i++)
		{
			this->points[i] = other.points[i];
		}
	}

private:
	std::vector<point>points;
};