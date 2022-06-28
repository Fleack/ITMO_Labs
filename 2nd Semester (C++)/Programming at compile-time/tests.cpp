#include "pch.h"
#include "Polynom.hpp"

TEST(Pow, Correct) 
{
	long long int result;

	result = polynom_pow<1, 0>::value;
	EXPECT_EQ(result, 1);

	result = polynom_pow<1, 1>::value;
	EXPECT_EQ(result, 1);

	result = polynom_pow<1, 111>::value;
	EXPECT_EQ(result, 1);

	result = polynom_pow<10, 0>::value;
	EXPECT_EQ(result, 1);

	result = polynom_pow<10, 1>::value;
	EXPECT_EQ(result, 10);

	result = polynom_pow<2, 2>::value;
	EXPECT_EQ(result, 4);

	result = polynom_pow<-2, 2>::value;
	EXPECT_EQ(result, 4);

	result = polynom_pow<-2, 3>::value;
	EXPECT_EQ(result, -8);

	result = polynom_pow<-2, 0>::value;
	EXPECT_EQ(result, 1);

	result = polynom_pow<0, 0>::value;
	EXPECT_EQ(result, 1);

	result = polynom_pow<2, 10>::value;
	EXPECT_EQ(result, 1024);
}

TEST(Polynom, Calculating)
{
	long long int result;

	result = polynom<1>::calc_y<0>::value;
	EXPECT_EQ(result, 1);

	result = polynom<1>::calc_y<1>::value;
	EXPECT_EQ(result, 1);

	result = polynom<1>::calc_y<-1>::value;
	EXPECT_EQ(result, 1);

	result = polynom<1,2>::calc_y<0>::value;
	EXPECT_EQ(result, 1);

	result = polynom<1,2>::calc_y<1>::value;
	EXPECT_EQ(result, 3);

	result = polynom<1,2>::calc_y<-1>::value;
	EXPECT_EQ(result, -1);

	result = polynom<1,-2>::calc_y<0>::value;
	EXPECT_EQ(result, 1);

	result = polynom<1,-2>::calc_y<1>::value;
	EXPECT_EQ(result, -1);

	result = polynom<1, -2>::calc_y<-1>::value;
	EXPECT_EQ(result, 3);

	result = polynom<1,2,3,4,5,6>::calc_y<0>::value;
	EXPECT_EQ(result, 1);

	result = polynom<1,2,3,4,5,6>::calc_y<1>::value;
	EXPECT_EQ(result, 21);

	result = polynom<1,2,3,4,5,6>::calc_y<2>::value;
	EXPECT_EQ(result, 321);

	result = polynom<1,-2,3,-4,5,-6>::calc_y<0>::value;
	EXPECT_EQ(result, 1);

	result = polynom<1, -2, 3, -4, 5, -6>::calc_y<1>::value;
	EXPECT_EQ(result, -3);

	result = polynom<1, -2, 3, -4, 5, -6>::calc_y<-1>::value;
	EXPECT_EQ(result, 21);

	result = polynom<0>::calc_y<0>::value;
	EXPECT_EQ(result, 0);
}

TEST(Polynom, output)
{
	std::string result;

	testing::internal::CaptureStdout();
	std::cout << polynom<1>();
	result = testing::internal::GetCapturedStdout();
	EXPECT_EQ(result, "1");

	testing::internal::CaptureStdout();
	std::cout << polynom<0>();
	result = testing::internal::GetCapturedStdout();
	EXPECT_EQ(result, "0");

	testing::internal::CaptureStdout();
	std::cout << polynom<1,2>();
	result = testing::internal::GetCapturedStdout();
	EXPECT_EQ(result, "1+2*x^1");

	testing::internal::CaptureStdout();
	std::cout << polynom<1,-2>();
	result = testing::internal::GetCapturedStdout();
	EXPECT_EQ(result, "1-2*x^1");

	testing::internal::CaptureStdout();
	std::cout << polynom<1,2,3,4,5>();
	result = testing::internal::GetCapturedStdout();
	EXPECT_EQ(result, "1+2*x^1+3*x^2+4*x^3+5*x^4");

	testing::internal::CaptureStdout();
	std::cout << polynom<1,-2,3,-4,5>();
	result = testing::internal::GetCapturedStdout();
	EXPECT_EQ(result, "1-2*x^1+3*x^2-4*x^3+5*x^4");

	testing::internal::CaptureStdout();
	std::cout << polynom<-1, -2, -3, -4>();
	result = testing::internal::GetCapturedStdout();
	EXPECT_EQ(result, "-1-2*x^1-3*x^2-4*x^3");

	testing::internal::CaptureStdout();
	std::cout << polynom<0,0,0,0,1>();
	result = testing::internal::GetCapturedStdout();
	EXPECT_EQ(result, "0+0*x^1+0*x^2+0*x^3+1*x^4");

	testing::internal::CaptureStdout();
	std::cout << polynom<1,0,0,-999>();
	result = testing::internal::GetCapturedStdout();
	EXPECT_EQ(result, "1+0*x^1+0*x^2-999*x^3");

	testing::internal::CaptureStdout();
	std::cout << polynom<0,1,2,3,4>();
	result = testing::internal::GetCapturedStdout();
	EXPECT_EQ(result, "0+1*x^1+2*x^2+3*x^3+4*x^4");
}