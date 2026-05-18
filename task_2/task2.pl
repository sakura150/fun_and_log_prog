
gcd(A, 0, A) :- !.
gcd(A, B, G) :-
    B > 0,
    R is A mod B,
    gcd(B, R, G).

factorial(N, F) :-
    factorial(N, 1, F).

factorial(0, Acc, Acc).
factorial(N, Acc, F) :-
    N > 0,
    N1 is N - 1,
    Acc1 is Acc * N,
    factorial(N1, Acc1, F).

f(M, N, Value) :-
    MN is M * N,
    f_iter(M, N, MN, 0, 0, Sum),
    Value is Sum // MN.

f_iter(_, _, MN, K, Sum, Sum) :-
    K >= MN, !.
f_iter(M, N, MN, K, Acc, Sum) :-
    K < MN,
    gcd(MN, K, G),
    CycleLen is MN // G,
    N mod CycleLen =:= 0,
    ColorCycles is N // CycleLen,
    M * ColorCycles =:= G,
    factorial(G, FactG),
    factorial(ColorCycles, FactCC),
    Pow is FactCC ** M,
    Term is FactG // Pow,
    K1 is K + 1,
    Acc1 is Acc + Term,
    f_iter(M, N, MN, K1, Acc1, Sum).
f_iter(M, N, MN, K, Acc, Sum) :-
    K < MN,
    K1 is K + 1,
    f_iter(M, N, MN, K1, Acc, Sum).

sum_all_f(Limit, Sum) :-
    sum_all_f_iter(2, 1, Limit, 0, Sum).

sum_all_f_iter(M, N, Limit, Acc, Sum) :-
    f(M, N, Val),
    Val =< Limit,
    !,
    Acc1 is Acc + Val,
    N1 is N + 1,
    sum_all_f_iter(M, N1, Limit, Acc1, Sum).
sum_all_f_iter(M, N, Limit, Acc, Sum) :-
    f(M, N, Val),
    Val > Limit,
    !,
    M1 is M + 1,
    sum_all_f_iter(M1, 1, Limit, Acc, Sum).
sum_all_f_iter(_, _, _, Acc, Acc).

main :-
    Limit = 1000000000000000,  % 10^15
    sum_all_f(Limit, Sum),
    write('Sum of all f(m,n) <= 10^15: '),
    write(Sum), nl.