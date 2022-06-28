#define _CRT_SECURE_NO_WARNINGS
#include<stdio.h>
#include<string.h>
#include<malloc.h>
#include<stdbool.h>
#include<stdlib.h>

FILE* file;

char* get_input_arc(char* argv[])
{
	int arc_name_len = (int)strlen(argv[1]) - 7;
	if (arc_name_len < 1)
	{
		printf("Enter correct /--file/ argument");
		exit(1);
	}
	char* arc_name = (char*)malloc(arc_name_len + 5);
	for (int i = 0; i < arc_name_len; i++)
	{
		arc_name[i] = argv[1][i + 7];
	}
	arc_name[arc_name_len] = '.'; arc_name[arc_name_len+1] = 'a'; arc_name[arc_name_len+2] = 'r'; arc_name[arc_name_len+3] = 'c';
	arc_name[arc_name_len+4] = '\0';
	return arc_name;
}

void get_all_files_names(char** all_files, char* argv[], int all_files_cnt)
{
	for (int i = 3; i < all_files_cnt+3; i++)
	{
		int file_name_len = strlen(argv[i]);
		char* file_name = malloc(file_name_len+1);
		for (int j = 0; j < file_name_len; ++j)
		{
			file_name[j] = argv[i][j];
		}
		file_name[file_name_len] = '\0';
		all_files[i - 3] = file_name;
	}
}

void open_file(FILE* temp, const char* name)
{
	if ((temp = fopen(name, "rb+")) == NULL)
	{
		printf("File wasn't opened");
		exit(1);
	}
}

void write_files_size(unsigned long long int files_chars_cnt)
{
	unsigned char sizes[10];
	int k = 9;
	while (k >= 0)
	{
		sizes[k] = files_chars_cnt % 16;
		files_chars_cnt = files_chars_cnt / 16;
		k--;
	}
	fwrite(sizes, 1, 10, file);
}

void write_files(char** all_files, int all_files_cnt)
{
	for (int i = 0; i < all_files_cnt; i++)
	{
		FILE* temp;
		if ((temp = fopen(all_files[i], "rb+")) == NULL)
		{
			printf("File wasn't opened");
			exit(1);
		}
		while (!feof(temp))
		{
			char a;
			fread(&a, 1, 1, temp);
			fwrite(&a, 1, 1, file);
		}
		fclose(temp);
	}
}

void data_write(char** all_files, int all_files_cnt)
{
	unsigned char total_cnt = all_files_cnt;
	char ARC1[4] = "ARC1";
	fwrite(&ARC1, 1, 4, file);
	fwrite(&total_cnt, 1, 1, file);
	for (int i = 0; i < all_files_cnt; i++)
	{
		long long int files_chars_cnt = 0;
		FILE* temp;
		if ((temp = fopen(all_files[i], "rb+")) == NULL)
		{
			printf("File wasn't opened");
			exit(1);
		}
		char name_len = strlen(all_files[i]);
		fwrite(&name_len, 1, 1, file);
		for (int j = 0; j < strlen(all_files[i]); j++)
		{
			fwrite(&all_files[i][j], 1, 1, file);
		}
		while (!feof(temp))
		{
			char a;
			fread(&a, 1, 1, temp);
			files_chars_cnt++;
		}
		fclose(temp);
		write_files_size(files_chars_cnt);
	}
	write_files(all_files, all_files_cnt);
}

void get_list()
{
	char ARC1[5]; ARC1[4] = '\0';
	fread(&ARC1, 1, 4, file);
	if (strcmp(ARC1, "ARC1") != 0)
	{
		printf("Not arc file!");
		exit(1);
	}
	unsigned char a;
	fread(&a, 1, 1, file);
	int all_files_cnt = a;
	printf("Files inside this arc:\n");
	for (int i = 0; i < all_files_cnt; i++)
	{
		char temp;
		fread(&temp, 1, 1, file);
		int len_name = temp;
		char* file_name = (char*)malloc(len_name * sizeof(char*)+1);
		file_name[len_name] = '\0';
		for (int i = 0; i < len_name; i++)
		{
			fread(&file_name[i], 1, 1, file);
		}
		printf("%s\n", file_name);
		char a;
		for (int i = 0; i < 10; i++)
		{
			fread(&a, 1, 1, file);
		}
		free(file_name);
	}
}

void free_all_files(char** all_files, int all_files_cnt)
{
	for (int i = 0; i < all_files_cnt; i++)
	{
		free(all_files[i]);
	}
	free(all_files);
}

unsigned long long int get_file_size(char _char_file_size[10])
{
	int result = 0;
	int start_index = 0;
	while (_char_file_size[start_index] == 0)
	{
		start_index++;
	}
	for (int i = start_index; i < 10; i++)
	{
		result = result * 16 + _char_file_size[i];
	}
	return result;
}

void extract_files()
{
	char ARC1[5]; ARC1[4] = '\0';
	fread(&ARC1, 1, 4, file);
	if (strcmp(ARC1, "ARC1") != 0)
	{
		printf("Not arc file!");
		exit(1);
	}
	unsigned char a;
	fread(&a, 1, 1, file);
	int all_files_cnt = a;
	unsigned long long int* file_sizes = (int*)malloc(all_files_cnt * sizeof(unsigned long long int));
	char** all_files = (char**)malloc(all_files_cnt * sizeof(char*));
	for (int i = 0; i < all_files_cnt; i++)
	{
		
	}
	for (int i = 0; i < all_files_cnt; i++)
	{
		char temp;
		fread(&temp, 1, 1, file);
		int len_name = temp;
		all_files[i] = (char*)malloc(len_name*sizeof(char) + 1);
		all_files[i][len_name] = '\0';
		for (int j = 0; j < len_name; j++)
		{
			fread(&all_files[i][j], 1, 1, file);
		}
		char _char_file_size[10];
		for (int j = 0; j < 10; j++)
		{
			fread(&_char_file_size[j], 1, 1, file);
		}
		file_sizes[i] = get_file_size(_char_file_size);
	}
	for (int i = 0; i < all_files_cnt; i++)
	{
		FILE* extract_file = NULL;
		if ((extract_file = fopen(all_files[i], "wb")) == NULL)
		{
			printf("File wasn't extracted");
			exit(1);
		}
		for (int j = 0; j < file_sizes[i]; ++j)
		{
			char a;
			fread(&a, 1, 1, file);
			fwrite(&a, 1, 1, extract_file);
		}
		fclose(extract_file);
	}
	free_all_files(all_files, all_files_cnt);
	free(file_sizes);
}

int main(int argc, char* argv[])
{
	if (argc < 3)
	{
		printf("Arguments are not correct!");
		printf("\nCorrect ones to create an archive: /--file *.arc/ /--create/ /_FILE_NAMES_/");
		printf("\nCorrect ones to extract: /--file *.arc/ /--extract/");
		printf("\nCorrect ones to show files in the archive: /--file *.arc/ /--list/");
		exit(1);
	}
	char* arc_name = get_input_arc(argv);
	if ((file = fopen(arc_name, "ab+")) == NULL)
	{
		printf("File wasn't created");
		exit(1);
	}
	if (strcmp(argv[2], "--create") == 0)
	{
		int all_files_cnt = argc - 3;
		char** all_files = (char**)malloc(all_files_cnt*sizeof(char*));
		get_all_files_names(all_files, argv, all_files_cnt);
		data_write(all_files, all_files_cnt);
		free_all_files(all_files, all_files_cnt);
	}
	else if (strcmp(argv[2], "--extract") == 0)
	{
		extract_files();
	}
	else if (strcmp(argv[2], "--list") == 0)
	{
		get_list();
	}
	if (file != NULL)
	{
		fclose(file);
	}
}