#define _CRT_SECURE_NO_WARNINGS
#include<stdio.h>
#include<string.h>
#include<time.h>
#include<malloc.h>
#include<stdbool.h>
#include<stdlib.h>

FILE* file;
typedef struct
{
	char ID3[3];
	char version[2];
	char flag;
	char size[4];
} ID3_Title;
ID3_Title CUR_ID3;
typedef struct
{
	char ID[4];
	char len[4];
	char flag[2];
	char code;
} Frame;

void get_big_endian(int num, char* length)
{
	num += 1;
	for (int i = 0; i < 4; ++i)
	{
		length[i] = (num >> (3 - i) * 7) & 127;
	}
}

int get_size_of_mp3info(char sizes[4]) {
	unsigned int result = 0;
	for (int i = 0; i < 4; ++i)
	{
		result += sizes[3 - i] << (i * 7);
	}
	return result;
}

void get_title_info()
{
	for (int i = 0; i < 10; i++)
	{
		char a;
		fread(&a, 1, sizeof(a), file);
		if (i < 3)
		{
			CUR_ID3.ID3[i] = a;
		}
		else if (i < 5)
		{
			CUR_ID3.version[i - 3] = a;
		}
		else if (i < 6)
		{
			CUR_ID3.flag = a;
		}
		else
		{
			CUR_ID3.size[i - 6] = a;
		}
	}
}



char* get_prop_name(char* argv[])
{
	int prop_len = ((int)strlen(argv[2])) - 6;
	if (prop_len != 4)
	{
		printf("Enter correct /--set=/ argument");
		exit(1);
	}
	char* prop_name = (char*)malloc(prop_len + 1);
	for (int i = 0; i < prop_len; i++)
	{
		prop_name[i] = argv[2][i + 6];
	}
	prop_name[prop_len] = '\0';
	return prop_name;
}

char* get_value(char* argv[])
{
	int value_len = ((int)strlen(argv[3])) - 8;
	if (value_len < 1)
	{
		printf("Enter correct /--value=/ argument");
		exit(1);
	}
	char* value = (char*)malloc(value_len + 1);
	for (int i = 0; i < value_len; i++)
	{
		value[i] = argv[3][i + 8];
	}
	value[value_len] = '\0';
	return value;
}

char* get_files_name(char* argv[])
{
	int files_name_len = ((int)strlen(argv[1])) - 11;
	if (files_name_len < 1)
	{
		printf("Enter correct /--filepath=/  argument");
		exit(1);
	}
	char* files_name = (char*)malloc(files_name_len + 1);
	for (int i = 0; i < files_name_len; i++)
	{
		files_name[i] = argv[1][i + 11];
	}
	files_name[files_name_len] = '\0';
	return files_name;
}

void show(bool everything, char* cmd)
{
	int size_of_metainf = get_size_of_mp3info(CUR_ID3.size);
	bool cmd_exists = false;
	printf("\n");
	while (size_of_metainf > 0)
	{
		char a;
		Frame* cur_frame = (Frame*)malloc(sizeof(Frame));
		for (int i = 0; i < 4; i++)
		{
			fread(&a, 1, sizeof(a), file);
			cur_frame->ID[i] = a;
		}
		if (cur_frame->ID[0] < 'A' || cur_frame->ID[0] > 'Z')
			return;
		for (int i = 0; i < 4; i++)
		{
			fread(&a, 1, sizeof(a), file);
			cur_frame->len[i] = a;
		}
		for (int i = 0; i < 2; i++)
		{
			fread(&a, 1, sizeof(a), file);
			cur_frame->flag[i] = a;
		}
		fread(&a, 1, sizeof(a), file);
		cur_frame->code = a;
		int size_of_frames_str = get_size_of_mp3info(cur_frame->len) - 1;
		//cur_frame->str[size_of_frames_str] = '\0';
		if (everything)
		{
			printf("%s\t", cur_frame->ID);
			char temp;
			for (int i = 0; i < size_of_frames_str; i++)
			{
				fread(&temp, 1, 1, file);
				printf("%c", temp);
			}
			printf("\n");
		}
		else
		{
			bool right_cmd = false;
			if (strcmp(cur_frame->ID, cmd) == 0)
			{
				right_cmd = true;
				cmd_exists = true;
			}
			if (right_cmd)
				printf("%s\t", cur_frame->ID);
			for (int i = 0; i < size_of_frames_str; i++)
			{
				char temp;
				fread(&temp, 1, 1, file);
				if (right_cmd)
					printf("%c", temp);
			}
			if (right_cmd)
			{
				printf("\n");
				return;
			}
		}
		free(cur_frame);
		size_of_metainf = size_of_metainf - 11 - size_of_frames_str;
	}
	if (!everything && !cmd_exists)
	{
		printf("There is no %s info!", cmd);
		exit(1);
	}
}

void skip_file(FILE* temp_file, char* file_name)
{
	char temp;
	while (!feof(file))
	{
		fread(&temp, 1, 1, file);
		fwrite(&temp, 1, 1, temp_file);
	}
	fclose(file);
	fclose(temp_file);
	remove(file_name);
	rename("temp.mp3", file_name);
	exit(0);
}

void set(char* argv[], char* file_name)
{
	FILE* temp_file = fopen("temp.mp3", "ab+");
	int size_of_metainf = get_size_of_mp3info(CUR_ID3.size);
	char* prop_name = get_prop_name(argv);
	unsigned char* value = get_value(argv);
	int value_len = strlen(value);
	fwrite(&CUR_ID3.ID3, 3, 1, temp_file); fwrite(&CUR_ID3.version, 2, 1, temp_file); fwrite(&CUR_ID3.flag, 1, 1, temp_file); fwrite(&CUR_ID3.size, 4, 1, temp_file);
	while (true)
	{
		char a;
		Frame* cur_frame = (Frame*)malloc(sizeof(Frame));
		for (int i = 0; i < 4; i++)
		{
			fread(&a, 1, sizeof(a), file);
			cur_frame->ID[i] = a;
		}
		if (cur_frame->ID[0] < 'A' || cur_frame->ID[0] > 'Z')
		{
			fwrite(&cur_frame->ID, 4, 1, temp_file);
			skip_file(temp_file, file_name);
		}
		for (int i = 0; i < 4; i++)
		{
			fread(&a, 1, sizeof(a), file);
			cur_frame->len[i] = a;
		}
		for (int i = 0; i < 2; i++)
		{
			fread(&a, 1, sizeof(a), file);
			cur_frame->flag[i] = a;
		}
		fread(&a, 1, sizeof(a), file);
		cur_frame->code = a;
		char value_lens[4];
		int size_of_frames_str = get_size_of_mp3info(cur_frame->len) - 1;
		fwrite(&cur_frame->ID, 4, 1, temp_file);
		if (strcmp(cur_frame->ID, prop_name) == 0)
		{
			get_big_endian(value_len, value_lens);
			fwrite(&value_lens, 4, 1, temp_file); fwrite(&cur_frame->flag, 2, 1, temp_file); fwrite(&cur_frame->code, 1, 1, temp_file);
			fwrite(value, value_len, 1, temp_file);
			char temp;
			for (int i = 0; i < size_of_frames_str; i++)
			{
				fread(&temp, 1, 1, file);
			}
		}
		else
		{
			fwrite(&cur_frame->len, 4, 1, temp_file); fwrite(&cur_frame->flag, 2, 1, temp_file); fwrite(&cur_frame->code, 1, 1, temp_file);
			char temp;
			for (int i = 0; i < size_of_frames_str; i++)
			{
				fread(&temp, 1, 1, file);
				fwrite(&temp, 1, 1, temp_file);
			}
		}
		free(cur_frame);
	}
}

int main(int argc, char* argv[])
{
	if (argc < 2)
	{
		printf("Arguments are not correct!");
		exit(1);
	}
	char* files_name = get_files_name(argv);
	if ((file = fopen(files_name, "rb+")) == NULL)
	{
		printf("File wasn't opened");
		exit(1);
	}
	if (strcmp(argv[2], "--show") == 0)
	{
		get_title_info();
		show(true, "");
	}
	else if (argv[2][2] == 'g')
	{
		get_title_info();
		char* prop_name = get_prop_name(argv);
		show(false, prop_name);
	}
	else if (argv[2][2] == 's')
	{
		get_title_info();
		set(argv, files_name);
	}
	if (file != NULL)
	{
		fclose(file);
	}
}
