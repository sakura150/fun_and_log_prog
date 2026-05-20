:- use_module(library(lists)).

% НОД по алгоритму Евклида
gcd(A, 0, A) :- A > 0, !.
gcd(A, B, G) :- B > 0, R is A mod B, gcd(B, R, G).

% Валидные параметры (M, N) для примитивных троек
valid_mn(M, N) :-
    M > N, N > 0,
    1 is (M - N) mod 2,
    gcd(M, N, 1).

% Генерация примитивных пифагоровых троек
prim_triple(MaxM, A, B, C) :-
    between(2, MaxM, M),
    between(1, M, N),
    valid_mn(M, N),
    A is M*M - N*N,
    B is 2*M*N,
    C is M*M + N*N.

% Проверка условия замощения
tiling_prim(MaxM, A, B, C) :-
    prim_triple(MaxM, A, B, C),
    Diff is abs(A - B),
    0 is C mod Diff.

% Основной предикат подсчёта
count_triangles(MaxPerimeter, Count) :-
    MaxM is floor(sqrt(MaxPerimeter / 2)) + 1,
    findall(KCount, (
        tiling_prim(MaxM, A0, B0, C0),
        Perim0 is A0 + B0 + C0,
        KCount is MaxPerimeter // Perim0
    ), Counts),
    sum_list(Counts, Count).

% Запуск с измерением времени (надёжный способ)
main(MaxPerimeter) :-
    format('Max perimeter: ~d~n', [MaxPerimeter]),
    %format('Calculating...~n'),
    
    % time/1 выводит статистику автоматически
    time(count_triangles(MaxPerimeter, Count)),
    
    format('~nTotal triangles: ~d~n', [Count]).







% По умолчанию
%main :- main(1000000).