#coding=utf-8
import numpy as np
from scipy.stats import norm

p = 2/3 # �������� p � ������������� ��������
epsilon = 0.01 # �������� ������� ��� ����������
delta = 0.05 # �������� ������ ��� �����������
n = int(p*(1-p)*1.96/epsilon) # ����������� ����� ������� �� ���
samples_count = 500 # ���-�� �������
count = 0  # ������� ���������� ���, ����� ���������� ������� ���������� �� ���. �������� ����� ��� �� epsilon

# ���������� samples_count �������
for i in range(samples_count):
    sample = np.random.binomial(n=1, p=p, size=n) # ���������� ������� � �������������� �������� � ���������� p ������ n
    sample_mean = np.mean(sample) # ���������� �������
    
    # ������� ����� �������, � ������� ���������� ������� ���������� �� ���. �������� ����� ��� �� epsilon
    if abs(sample_mean - p) > epsilon:
        count += 1

print(count)