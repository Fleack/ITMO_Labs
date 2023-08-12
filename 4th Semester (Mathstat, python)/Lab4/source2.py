#coding=utf-8
import pandas as pd
import matplotlib.pyplot as plt

from scipy.stats import mannwhitneyu, ks_2samp

df = pd.read_csv('song_data.csv')

song_threshold_seconds = 240

# Две подвыборки по длительности песен
short_songs = df[df['song_duration_ms'] < song_threshold_seconds*1000]
long_songs = df[df['song_duration_ms'] >= song_threshold_seconds*1000]

# Тест Колмогорова–Смирнова
_, p_k = ks_2samp(short_songs['song_popularity'], long_songs['song_popularity'])
print('Kolmogorov-Smirnov:')
print('p-value: ', p_k)
print()

# Тест Уилкоксона-Манна-Уитни
_, p_m= mannwhitneyu(short_songs['song_popularity'], long_songs['song_popularity'], alternative='two-sided')
print(f'Mann-Whitneyu: ')
print('p-value: ', p_m)
print()

# Строим графики для наглядности
plt.hist(short_songs['song_popularity'], bins=25, edgecolor='black')
plt.xlabel('Popularity')
plt.ylabel('Count')
plt.title('Short Song Popularity Distribution')
plt.show()

plt.hist(long_songs['song_popularity'], bins=25, edgecolor='black')
plt.xlabel('Popularity')
plt.ylabel('Count')
plt.title('Long Song Popularity Distribution')
plt.show()