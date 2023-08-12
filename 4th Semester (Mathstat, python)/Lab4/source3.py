#coding=utf-8
import pandas as pd
import matplotlib.pyplot as plt

from scipy.stats import kendalltau, spearmanr


# ������ ����
df = pd.read_csv('song_data.csv')

# ����������� ���������� ��������
corr_k, p_k = kendalltau(df["song_popularity"], df["song_duration_ms"])
print("Kendall:")
print("Kendall Correlation Coefficient:", corr_k)
print('p-value: ', p_k)
print()

# ����������� ���������� ��������
corr_s, p_s = spearmanr(df["song_popularity"], df["song_duration_ms"])
print("Spearman:")
print("Spearman Correlation Coefficient:", corr_s)
print('p-value: ', p_s)
print()

# ������ ��� �����������
plt.scatter(df["song_popularity"], df["song_duration_ms"], s=5)
plt.xlabel('Song popularity')
plt.ylabel('Song duration (ms)')
plt.show()