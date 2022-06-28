#define _CRT_SECURE_NO_WARNINGS
#include<stdio.h>
#include<string.h>
#include<malloc.h>
#include<stdbool.h>
#include<stdlib.h>

FILE* file;
typedef struct
{
	unsigned char bmp_sign[2];
	unsigned char files_size[4];
	unsigned char reserved_zeroes[4];
	unsigned char pixels_data_pos[4];
} BMP_HEADER;
BMP_HEADER CUR_BMP;
typedef struct
{
	unsigned char size[4];
	unsigned char width[4];
	unsigned char height[4];
	unsigned char planes[2];
	unsigned char bit_cnt[2];
	unsigned char compression[4];
	unsigned char img_size[4];
	unsigned char x_pixels[4];
	unsigned char y_pixels[4];
	unsigned char clr_used[4];
	unsigned char clr_important[4];
} BMP_INFO;
BMP_INFO CUR_INFO;

char* get_input_bmp(char* argv[])
{
	int bmp_name_len = (int)strlen(argv[1])-8;
	if (bmp_name_len < 1)
	{
		printf("Enter correct /--input/ argument");
		exit(1);
	}
	char* bmp_name = (char*)malloc(bmp_name_len+1);
	for (int i = 0; i < bmp_name_len; i++)
	{
		bmp_name[i] = argv[1][i+8]; // --input a.bmp\0;
	}
	bmp_name[bmp_name_len] = '\0';
	return bmp_name;
}

char* get_output_bmp(char* argv[])
{
	int output_dir_len = (int)strlen(argv[2]) - 9;
	if (output_dir_len < 1)
	{
		printf("Enter correct /--output/ argument");
		exit(1);
	}
	char* output_name = (char*)malloc(output_dir_len + 11); // --output c:/ab/dadad/00000.bmp\0
	for (int i = 0; i < output_dir_len; i++)
	{
		output_name[i] = argv[2][i+9];
	}
	output_name[output_dir_len] = '\\';
	for (int i = output_dir_len+1; i < output_dir_len+6; i++)
	{
		output_name[i] = '0';
	}
	output_name[output_dir_len + 6] = '.'; output_name[output_dir_len + 7] = 'b'; output_name[output_dir_len + 8] = 'm'; output_name[output_dir_len + 9] = 'p';
	output_name[output_dir_len + 10] = '\0';
	return output_name;
}

int get_max_iter(char* argv[])
{
	int arg_size = (int)strlen(argv[3]);
	if (arg_size < 12)
	{
		printf("Enter correct /--max_iter/ argument");
		exit(1);
	}
	int result = 0;
	for (int i = 11; i < arg_size; i++)
	{
		result = result * 10 + (argv[3][i] - '0');
	}
	return result;
}

int get_dump_freq(char* argv[])
{
	int arg_size = (int)strlen(argv[4]);
	if (arg_size < 13)
	{
		printf("Enter correct /--dump_freq/ argument");
		exit(1);
	}
	int result = 0;
	for (int i = 12; i < arg_size; i++)
	{
		result = result * 10 + (argv[4][i] - '0');
	}
	return result;
}

void get_header_info()
{
	for (int i = 0; i < 14; i++)
	{
		char a;
		fread(&a, 1, sizeof(a), file);
		if (i < 2)
		{
			CUR_BMP.bmp_sign[i] = a;
		}
		else if (i < 6)
		{
			CUR_BMP.files_size[i-2] = a;
		}
		else if (i < 10)
		{
			CUR_BMP.reserved_zeroes[i - 6] = a;
		}
		else
		{
			CUR_BMP.pixels_data_pos[i - 10] = a;
		}
	}
}



void get_bmp_info()
{
	for (int i = 0; i < 4; i++)
	{
		char a;
		fread(&a, 1, sizeof(a), file);
		CUR_INFO.size[i] = a;
	}
	int size_of_info = CUR_INFO.size[0];
	for (int i = 0; i < size_of_info-4; i++)
	{
		char a;
		fread(&a, 1, sizeof(a), file);
		if (i < 4)
		{
			CUR_INFO.width[i] = a;
		}
		else if (i < 8)
		{
			CUR_INFO.height[i - 4] = a;
		}
		else if (i < 10)
		{
			CUR_INFO.planes[i - 8] = a;
		}
		else if (i < 12)
		{
			CUR_INFO.bit_cnt[i - 10] = a;
		}
		else if (i < 16)
		{
			CUR_INFO.compression[i - 12] = a;
		}
		else if (i < 20)
		{
			CUR_INFO.img_size[i - 16] = a;
		}
		else if (i < 24)
		{
			CUR_INFO.y_pixels[i - 20] = a;
		}
		else if (i < 28)
		{
			CUR_INFO.x_pixels[i - 24] = a;
		}
		else if (i < 32)
		{
			CUR_INFO.clr_used[i - 28] = a;
		}
		else if (i < 36)
		{
			CUR_INFO.clr_important[i - 32] = a;
		}
	}
}

int get_height_int()
{
	int nums[8];
	for (int i = 0; i < 4; i++)
	{
		int temp = CUR_INFO.height[i];
		nums[7 - i * 2] = temp % 16;
		temp /= 16;
		nums[7 - i * 2 - 1] = temp % 16;
	}

	int result = 0;
	int degree = 1;
	int start = -1;
	for (int i = 0; i < 8; ++i)
	{
		if (nums[i] != 0)
		{
			start = i;
			break;
		}
	}
	if (start < 0)
	{
		printf("Incorrect bmp file!");
		exit(1);
	}
	for (int i = 7; i >= start; --i)
	{
		result += nums[i] * degree;
		degree *= 16;
	}
	return result;
}

int get_width_int()
{
	int nums[8];
	for (int i = 0; i < 4; i++)
	{
		int temp = CUR_INFO.width[i];
		nums[7-i*2] = temp % 16;
		temp /= 16;
		nums[7-i*2-1] = temp % 16;
	}

	int result = 0;
	int degree = 1;
	int start = -1;
	for (int i = 0; i < 8; ++i)
	{
		if (nums[i] != 0)
		{
			start = i;
			break;
		}
	}
	if (start < 0)
	{
		printf("Incorrect bmp file!");
		exit(1);
	}
	for (int i = 7; i >= start; --i)
	{
		result += nums[i]* degree;
		degree *= 16;
	}
	return result;
}

void save_img(int** img, char* output_bmp, int height_int, int width_int, char* input_bmp)
{
	FILE* output;
	int output_name_len = strlen(output_bmp);
	int digit = 1;
	for (int i = output_name_len-5; i >= output_name_len-9; --i)
	{
		if (output_bmp[i]+digit > '9')
		{
			if (i == output_name_len - 9)
			{
				printf("Too many saved files!");
				exit(1);
			}
			output_bmp[i] = '0';
			digit = 1;
		}
		else
		{
			output_bmp[i]++;
			break;
		}
	}
	if ((output = fopen(output_bmp, "wb")) == NULL)
	{
		printf("File was not created");
		exit(1);
	}
	FILE* temp;
	if ((temp = fopen(input_bmp, "r")) == NULL)
	{
		printf("Something wrong with source-file!");
		exit(1);
	}
	for (int i = 0; i < 18; i++)
	{
		char a;
		fread(&a, 1, 1, temp);
		fwrite(&a, 1, 1, output);
	}
	int size_of_info = CUR_INFO.size[0];
	for (int i = 0; i < size_of_info - 4; i++)
	{
		char a;
		fread(&a, 1, 1, temp);
		fwrite(&a, 1, 1, output);
	}
	int useless_bytes = (4 - ((width_int * 3) % 4));
	for (int i = height_int-1; i >= 0; --i)
	{
		for (int j = 0; j < width_int; j++)
		{
			if (img[i][j] == 1)
			{
				char a = 0;
				fwrite(&a, 1, 1, output); fwrite(&a, 1, 1, output); fwrite(&a, 1, 1, output);
			}
			else
			{
				char a = 255;
				fwrite(&a, 1, 1, output); fwrite(&a, 1, 1, output); fwrite(&a, 1, 1, output);
			}
		}
		for (int j = 0; j < useless_bytes; j++)
		{
			char a = 0;
			fwrite(&a, 1, 1, output);
		}
	}
	fclose(temp);
	fclose(output);
}

int get_neighbors_cnt(int** img, int i, int j, int height_int, int width_int)
{
	int neighbors_cnt = 0;
	bool j_minus_1 = false;
	bool j_plus_one = false;
	bool i_minus_one = false;
	bool i_plus_one = false;
	//проверка выхода за массив
	if (j-1 >= 0)
	{
		j_minus_1 = true;
	}
	if (j + 1 < width_int)
	{
		j_plus_one = true;
	}
	if (i - 1 >= 0)
	{
		i_minus_one = true;
	}
	if (i+1 < height_int)
	{
		i_plus_one = true;
	}
	//подсчёт кол-во соседей
	if (i_minus_one && j_minus_1)
	{
		if (img[i-1][j-1] == 1)
		{
			neighbors_cnt++;
		}
	}
	if (j_minus_1)
	{
		if (img[i][j-1] == 1)
		{
			neighbors_cnt++;
		}
	}
	if (i_minus_one)
	{
		if (img[i-1][j] == 1)
		{
			neighbors_cnt++;
		}
	}
	if (i_plus_one && j_minus_1)
	{
		if (img[i + 1][j-1] == 1)
		{
			neighbors_cnt++;
		}
	}
	if (i_plus_one)
	{
		if (img[i+1][j] == 1)
		{
			neighbors_cnt++;
		}
	}
	if (i_plus_one && j_plus_one)
	{
		if (img[i + 1][j + 1] == 1)
		{
			neighbors_cnt++;
		}
	}
	if (j_plus_one)
	{
		if (img[i][j + 1] == 1)
		{
			neighbors_cnt++;
		}
	}
	if (i_minus_one && j_plus_one)
	{
		if (img[i - 1][j + 1] == 1)
		{
			neighbors_cnt++;
		}
	}
	return neighbors_cnt;
}

void game(int** img, int max_iter, int dump_freq, int height_int, int width_int, char* output_bmp, char* input_bmp)
{
	int** new_img = (int**)malloc(height_int * sizeof(int*));
	for (int i = 0; i < height_int; i++)
	{
		new_img[i] = (int*)malloc(width_int * sizeof(int));
	}
	for (int k = 0; k < max_iter; k++)
	{
		for (int i = 0; i < height_int; i++)
		{
			for (int j = 0; j < width_int; j++)
			{
				int neighbors_cnt = get_neighbors_cnt(img, i, j, height_int, width_int);
				if (img[i][j] == 1)
				{
					if (neighbors_cnt < 2 || neighbors_cnt > 3)
					{
						new_img[i][j] = 0;
					}
					else
					{
						new_img[i][j] = 1;
					}
				}
				else
				{
					if (neighbors_cnt == 3)
					{
						new_img[i][j] = 1;
					}
					else
					{
						new_img[i][j] = 0;
					}
				}
			}
		}
		printf("\n\n\n");
		for (int i = 0; i < height_int; i++)
		{
			for (int j = 0; j < width_int; j++)
			{
				img[i][j] = new_img[i][j];
				printf("%d ", img[i][j]);
			}
			printf("\n");
		}
		if ((k+1)%dump_freq == 0)
		{
			save_img(new_img, output_bmp, height_int, width_int, input_bmp);
		}
	}
	for (int i = 0; i < width_int; i++)
	{
		free(img[i]);
		free(new_img[i]);
	}
	free(img);
	free(new_img);
}

void parse(int max_iter, int dump_freq, char* output_bmp, char* input_bmp)
{
	get_header_info();
	get_bmp_info();
	int width_int = get_width_int();
	int height_int = get_height_int();
	int useless_bytes = (4 - ((width_int * 3) % 4));
	int** img = (int**)malloc(height_int * sizeof(int*));
	for (int i = 0; i < height_int; i++)
	{
		img[i] = (int*)malloc(width_int * sizeof(int));
	}
	for (int i = height_int - 1; i >= 0; --i)
	{
		for (int j = 0; j < width_int; j++)
		{
			unsigned char RGB[3];
			fread(&RGB[0], 1, sizeof(char), file);
			fread(&RGB[1], 1, sizeof(char), file);
			fread(&RGB[2], 1, sizeof(char), file);
			if (RGB[0] == 255 && RGB[1] == 255 && RGB[2] == 255)
			{
				img[i][j] = 0;
			}
			else
			{
				img[i][j] = 1;
			}
		}
		for (int i = 0; i < useless_bytes; i++)
		{
			char temp;
			fread(&temp, 1, sizeof(char), file);
		}
	}
	for (int i = 0; i < height_int; i++)
	{
		for (int j = 0; j < width_int; j++)
		{
			printf("%d ", img[i][j]);
		}
		printf("\n");
	}
	game(img, max_iter, dump_freq, height_int, width_int, output_bmp, input_bmp);
}

int main(int argc, char* argv[])
{
	if (argc < 3)
	{
		printf("Arguments are not correct!");
		printf("\nCorrect ones: /--input input_file.bmp/ /--output dir_name/ /--max_iter N/ /--dump_freq N/");
		exit(1);
	}
	char* input_bmp = get_input_bmp(argv);
	if ((file = fopen(input_bmp, "rb+")) == NULL)
	{
		printf("File wasn't opened");
		exit(1);
	}
	char* output_bmp = get_output_bmp(argv);
	int max_iter = 2147483647;
	if (argc > 3)
	{
		max_iter = get_max_iter(argv);
	}
	int dump_freq = 1;
	if (argc > 4)
	{
		dump_freq = get_dump_freq(argv);
	}
	parse(max_iter, dump_freq, output_bmp, input_bmp);
	if (file != NULL)
	{
		fclose(file);
	}
}