from munkres import Munkres, print_matrix

matrix = [[5, 9, 1],
          [10, 3, 2],
          [8, 7, 4]]
matrix2 = [[100, 10, 40, 15, 0, 0],
            [10, 100, 20, 5, 0, 0],
            [40, 20, 100, 30, 0, 0],
            [15, 5, 30, 100, 0, 0],
            [35, 25, 35, 30, 0, 0],
            [10, 40, 15, 5, 0, 0]]
m = Munkres()
indexes = m.compute(matrix2)
print_matrix(matrix2, msg='Lowest cost through this matrix:')
total = 0
for row, column in indexes:
    value = matrix2[row][column]
    total += value
    print(f'({row}, {column}) -> {value}')
print(f'total cost: {total}')