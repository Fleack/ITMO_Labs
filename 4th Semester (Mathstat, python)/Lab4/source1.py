#coding=utf-8
import pandas as pd
import matplotlib.pyplot as plt

from scipy.stats import shapiro, kstest

# ������ ����
df = pd.read_csv('song_data.csv')

# ������� ������� � ���
mean = df['song_popularity'].mean()
std = df['song_popularity'].std()

# �������� ������-�����
_, p_s = shapiro(df['song_popularity'])
print('Shapiro-Wilk:')
print('p-value: ', p_s)
print()

# �������� �����������-��������
_, p_k = kstest(df['song_popularity'], 'norm', args=(mean, std))
print('Kolmogorov-Smirnov:')
print('p-value: ', p_k)
print()

# ������ ������ ��� �����������
plt.hist(df['song_popularity'], bins=25, edgecolor='black')
plt.xlabel('Popularity')
plt.ylabel('Count')
plt.title('Song Popularity Distribution')
plt.show()