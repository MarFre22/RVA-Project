import numpy as np
import pandas as pd


def shrink(data, rows, cols):
    return data.reshape(rows, data.shape[0]/rows, cols, data.shape[1]/cols).mean(axis=1).mean(axis=2)

path = '/Volumes/Dados/Uni - 5o Ano (1o Semestre)/RVA - Realidade Virtual e Aumentada/5- Projetos e Apresentações/2022-23/Projeto - Maquete Poluição AR - Álvaro, David/Tema - Recursos/0- Dados Maquete/NCM8/48.dat'
#path = '/Volumes/Dados/Uni - 5o Ano (1o Semestre)/RVA - Realidade Virtual e Aumentada/5- Projetos e Apresentações/2022-23/Projeto - Maquete Poluição AR - Álvaro, David/Tema - Recursos/0- Dados Maquete/NCM2/48.dat'
'''
data = np.genfromtxt(path,
                     skip_header=1,
                     skip_footer=1,
                     names=True,
                     dtype=None,
                     delimiter=' ')
'''
df = pd.read_csv(path,
            sep='\s\s+', engine='python')

#df = df.drop(['x', 'y'], axis=1)

df = df.dropna()

df = df.apply(pd.to_numeric)

pd.options.display.float_format = '{:.1f}'.format

print(df)

#headers = df.columns.tolist()

#np.savetxt(r'02edit.dat', df.values, fmt='%d', delimiter=' ')




# ----------------------------------------------------------------
# Cont rows and columns
n_rows = []
n_columns = []

dic = {
    "x": [],
    "y": [],
    "vel": [],
    "dir": []}


cont_rows = 0.0
cont_columns = -1.0
for i in range(1, len(df)):
    x = df.iloc[ i-1:i, 0:1].values
    y = df.iloc[ i-1:i, 1:2].values
    vel = df.iloc[ i-1:i, 2:3].values
    dire = df.iloc[ i-1:i, 3:4].values

    cont_columns += 1 

    dic["x"].append(cont_rows)
    dic["y"].append(cont_columns)
    dic["vel"].append(vel[0][0])
    dic["dir"].append(dire[0][0])

    #print(x)
    #print(y)

    

    if y == 668.0:
        n_columns.append(cont_columns)
        cont_columns = 0.0

        cont_rows += 1
    
    if x == 668.0:
        n_rows.append(cont_rows)


new_df = pd.DataFrame(dic)

print(new_df)

#np.savetxt(r'02edit.dat', df.values, fmt='%d', delimiter=' ')

with open('48edit.dat', 'w') as tfile:
    tfile.write(new_df.to_string(index=False))


exit()

#print(n_rows)
#print(n_columns)

'''

n_rows = n_rows[0]+1
print(n_rows)
n_columns = n_columns[0]+1
print(n_columns)
vel_matrix = np.zeros((n_columns, n_rows), dtype=float)

print(vel_matrix)


cont_rows = 0
cont_columns = -1
for i in range(1, len(df)):
    x = df.iloc[ i-1:i, 0:1].values
    y = df.iloc[ i-1:i, 1:2].values
    vel = df.iloc[ i-1:i, 2:3].values

    #print(vel)
    #print(vel[0][0])

    #print(i)
    #print(cont_columns)
    #print(cont_rows)

    vel_matrix[cont_columns][cont_rows] = vel[0][0]

    
    cont_columns += 1

    if y == 668.0:
        cont_columns = 0

        cont_rows += 1

    

print(vel_matrix)


#reduced_vel_matrix = shrink(vel_matrix, 167, 167)

reduced_vel_matrix = vel_matrix

print(reduced_vel_matrix)
print(len(reduced[0]))

'''
