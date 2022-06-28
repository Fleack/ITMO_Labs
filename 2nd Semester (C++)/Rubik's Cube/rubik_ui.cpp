#include "rubik_ui.h"

void get_help()
{
	system("cls");
	std::cout << "//////////////////////////////////////////////////////////////////////////////////////////////////////////////////\n";
	std::cout << "// Вы находитесь в меню помощи.                                                                                 //\n";
	std::cout << "// Все доступные команды:                                                                                       //\n";
	std::cout << "// help - Выводит меню помощи                                                                                   //\n";
	std::cout << "// print - Выводит текущее состояние кубика                                                                     //\n";
	std::cout << "// save - записывает текущее состояние кубика в указанный файл                                                  //\n";
	std::cout << "// 90R - Делает поворот грани кубика на 90 градусов по часовой стрелке, после команды вводится номер грани      //\n";
	std::cout << "// 90L - Делает поворот грани кубика на 90 градусов против часовой стрелке, после команды вводится номер грани  //\n";
	std::cout << "// 180 - Делает поворот грани кубика на 180 градусов, после команды вводится номер грани                        //\n";
	std::cout << "// exit - выключает программу                                                                                   //\n";
	std::cout << "//////////////////////////////////////////////////////////////////////////////////////////////////////////////////\n";
}

void cube_control(RCube& rubik)
{
	system("cls");
	bool IsChanged = false;
	std::cout << "Вы находитесь в меню управления кубиком.\nДля получения дополнительной информации напишите в консоль [help]\n";
	while (true)
	{
		std::cout << "Введите команду...\n";
		std::string command;
		std::cin >> command;
		if (command == "help")
		{
			get_help();
		}
		else if (command == "print")
		{
			rubik.print_cur_condition();
		}
		else if (command == "save")
		{
			std::string FileName;
			std::cout << "Пожалуйста укажите название файла для сохранения: ";
			std::cin >> FileName;
			std::ofstream out(FileName);
			rubik.save_cur_condition(out);
			std::cout << "Успешно сохранено!\n";
			IsChanged = false;
		}
		else if (command == "exit")
		{
			if (IsChanged)
			{
				system("cls");
				std::string user_choice;
				std::cout << "Внимание, есть несохранённые изменения! Желаете их сохранить? [YES]/[NO]\n";
				std::cin >> user_choice;
				while (user_choice != "YES" && user_choice != "NO")
				{
					system("cls");
					std::cout << "Неверная команда, введите [YES] или [NO]\n";
					std::cin >> user_choice;
				}
				if (user_choice == "YES")
				{
					std::string FileName;
					std::cout << "Пожалуйста укажите название файла для сохранения: ";
					std::cin >> FileName;
					std::ofstream out(FileName);
					rubik.save_cur_condition(out);
					std::cout << "Успешно сохранено!\n";
				}
				exit(0);
			}
		}
		else if (command == "90R")
		{
			std::cout << "Введите номер грани для поворота на 90 градусов по часовой стрелке\n";
			int cube_face_id;
			std::cin >> cube_face_id;
			if (cube_face_id >= 1 && cube_face_id <= 6)
			{
				rubik.rotate90_r(cube_face_id - 1);
				IsChanged = true;
				std::cout << "Грань успешно повернута!\n";
			}
			else
			{
				std::cout << "Введен неправильный номер грани, команда отменена\n";
			}
		}
		else if (command == "90L")
		{
			std::cout << "Введите номер грани для поворота на 90 градусов против часовой стрелке\n";
			int cube_face_id;
			std::cin >> cube_face_id;
			if (cube_face_id >= 1 && cube_face_id <= 6)
			{
				rubik.rotate90_l(cube_face_id - 1);
				IsChanged = true;
				std::cout << "Грань успешно повернута!\n";
			}
			else
			{
				std::cout << "Введен неправильный номер грани, команда отменена\n";
			}
		}
		else if (command == "180")
		{
			std::cout << "Введите номер грани для поворота на 180 градусов\n";
			int cube_face_id;
			std::cin >> cube_face_id;
			if (cube_face_id >= 1 && cube_face_id <= 6)
			{
				rubik.rotate180(cube_face_id - 1);
				IsChanged = true;
				std::cout << "Грань успешно повернута!\n";
			}
			else
			{
				std::cout << "Введен неправильный номер грани, команда отменена\n";
			}
		}
		else
		{
			std::cout << "Такой команды не существует, напишите [help] для получения информации о всех командах!\n";
		}
	}
}

void console_rubik(RCube& rubik)
{
	system("cls");
	std::cout << "Желаете случайно перемешать кубик или считать состояние из файла?\n";
	std::cout << "Для случайного перемешивания напишите [scramble] | Для считывания из файла напишите [file] | Для пропуска напишите [skip]\n";
	int Scramble_or_File_or_Skip;
	while (true)
	{
		std::string user_choice;
		std::cin >> user_choice;
		if (user_choice == "scramble")
		{
			Scramble_or_File_or_Skip = 1;
			break;
		}
		else if (user_choice == "file")
		{
			Scramble_or_File_or_Skip = 2;
			break;
		}
		else if (user_choice == "skip")
		{
			Scramble_or_File_or_Skip = 0;
			break;
		}
		else
		{
			system("cls");
			std::cout << "Неверная команда!\n";
			std::cout << "Для случайного перемешивания напишите [scramble] | Для считывания из файла напишите [file] | Для пропуска напишите [skip]\n";
		}
	}
	system("cls");
	if (Scramble_or_File_or_Skip == 1)
	{
		rubik.scramble();
		std::cout << "Кубик был успешно перемешан!\n";
	}
	else if (Scramble_or_File_or_Skip == 2)
	{
		while (true)
		{
			std::string FileName;
			std::cout << "Пожалуйста укажите название файла: ";
			std::cin >> FileName;
			std::ifstream in(FileName);
			rubik.file_input(in);
			if (!rubik.IsCorrect())
			{
				std::cout << "ВНИМАНИЕ!!!\nВаш кубик рубика имеет некорректное положение цветов, его сборка невозможна без механического разбора.\n";
				std::cout << "Укажите файл с корректным положение цветов!\n";
			}
			else
				break;
		}
	}
	bool Control_or_Solve;
	std::cout << "Желаете самостоятельно вращать грани кубика или включить автоматическую сборку?\n";
	std::cout << "Для самостоятельного управления кубиком напишите [control] | Для автоматической сборка напишите [solve]\n";
	while (true)
	{
		std::string user_choice;
		std::cin >> user_choice;
		if (user_choice == "control")
		{
			Control_or_Solve = true;
			break;
		}
		else if (user_choice == "solve")
		{
			Control_or_Solve = false;
			break;
		}
		else
		{
			system("cls");
			std::cout << "Неверная команда!\n";
			std::cout << "Для самостоятельного управления кубиком напишите [control] | Для автоматической сборка напишите [solve]\n";
		}
	}
	if (Control_or_Solve)
	{
		cube_control(rubik);
	}
	else
	{
		rubik.solve(); // rubik solving
		std::cout << "\nАлгоритм сборки кубика рубика:\n\n";
		rubik.print_solution();
		std::cout << "\n";
	}
}

void start_rubik()
{
	setlocale(LC_ALL, "Russian");
	RCube rubik;
	std::cout << "Привет, это полностью функциональный кубик рубика!\nПрограмма может работать через консольные команды, либо же через GUI\n";
	std::cout << "Для работы через консоль напишите [console] | Для работы через GUI напишите [GUI]\n";
	bool Console_or_GUI;
	while (true)
	{
		std::string user_choice;
		std::cin >> user_choice;
		if (user_choice == "console")
		{
			Console_or_GUI = true;
			break;
		}
		else if (user_choice == "GUI")
		{
			Console_or_GUI = false;
			std::cout << "\n\nGUI Пока в разработке :)\n\n";
			//break; // GUI Пока в разработке :)
		}
		else
		{
			system("cls");
			std::cout << "Неверная команда!\n";
			std::cout << "Для работы через консоль напишите [console] | Для работы через GUI напишите [GUI]\n";
		}
	}
	if (Console_or_GUI)
	{
		console_rubik(rubik);
	}
	else
	{
		// gui func
	}
}