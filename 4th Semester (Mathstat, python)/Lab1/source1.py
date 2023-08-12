#coding=utf-8
import pandas as pd
import numpy as np
import matplotlib.pyplot as plt
import seaborn as sns

# Импорт данных и форматирование
df = pd.read_csv("sex_bmi_smokers.csv", header=None)
df = df[0].str.split(',', expand=True)
df.columns = ["sex", "bmi", "smoker"]
df = df.drop(index=0)

# Группировка по курит/не курит
smokers_yes = df.loc[df["smoker"] == "\"yes\""]
smokers_no = df.loc[df["smoker"] == "\"no\""]

# Группировка по всем комбинациям пол-курящий
male_smokers_yes = smokers_yes.loc[smokers_yes["sex"] == "male"]
male_smokers_no = smokers_no.loc[smokers_no["sex"] == "male"]
female_smokers_yes = smokers_yes.loc[smokers_yes["sex"] == "female"]
female_smokers_no = smokers_no.loc[smokers_no["sex"] == "female"]

# Выбор bmi из всех данных
bmi = pd.to_numeric(df["bmi"], errors="coerce")
male_smokers_yes_bmi = pd.to_numeric(male_smokers_yes["bmi"], errors="coerce")
male_smokers_no_bmi = pd.to_numeric(male_smokers_no["bmi"], errors="coerce")
female_smokers_yes_bmi = pd.to_numeric(female_smokers_yes["bmi"], errors="coerce")
female_smokers_no_bmi = pd.to_numeric(female_smokers_no["bmi"], errors="coerce")

# Объединение в массив
arr = np.array([male_smokers_yes_bmi, male_smokers_no_bmi, female_smokers_yes_bmi, female_smokers_no_bmi], dtype=object)

# Вычисление (выбороч.) среднего, дисперсии, медианы и квартили 3/5 для всех групп
cnt = 0
for group in arr:
    if cnt == 0:
        print("Male smokers_yes:")
    if cnt == 1:
        print("Male smokers_no:")
    if cnt == 2:
        print("Female smokers_yes:")
    if cnt == 3:
        print("Female smokers_no:")

    print("BMI Mean:", group.mean())
    print("BMI Var:", group.var())
    print("BMI Median:", group.median())
    print("BMI Quantile:", group.quantile(3/5))
    print()
    cnt += 1



# Построение графиков

# График эмперической функции распределения
sns.ecdfplot(bmi)
plt.xlabel("BMI")
plt.ylabel("ECDF")
plt.title('Эмпирическая функция распределения для всех наблюдателей')
plt.show()

# Гистограмма
sns.histplot(bmi, kde=True)
plt.xlabel("BMI")
plt.ylabel("Count")
plt.title('Гистограмма для всех наблюдателей')
plt.show()

# Box-plot
sns.boxplot(bmi.to_numpy())
plt.title('Box-plot для всех наблюдателей')
plt.show()