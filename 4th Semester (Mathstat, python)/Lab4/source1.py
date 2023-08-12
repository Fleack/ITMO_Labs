#coding=utf-8
import pandas as pd
import matplotlib.pyplot as plt

from scipy.stats import shapiro, kstest

# Читаем файл
df = pd.read_csv('song_data.csv')

# Считаем среднее и ско
mean = df['song_popularity'].mean()
std = df['song_popularity'].std()

# Критерий Шапиро-Уилка
_, p_s = shapiro(df['song_popularity'])
print('Shapiro-Wilk:')
print('p-value: ', p_s)
print()

# Критерий Колмогорова-Смирнова
_, p_k = kstest(df['song_popularity'], 'norm', args=(mean, std))
print('Kolmogorov-Smirnov:')
print('p-value: ', p_k)
print()

# Строим график для наглядности
plt.hist(df['song_popularity'], bins=25, edgecolor='black')
plt.xlabel('Popularity')
plt.ylabel('Count')
plt.title('Song Popularity Distribution')
plt.show()