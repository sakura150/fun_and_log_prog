gcd(A, 0, A) :- !.
gcd(A, B, G) :-
    B \= 0,
    R is A mod B,
    gcd(B, R, G).

is_square(N) :-
    N >= 0,
    S is floor(sqrt(N)),
    S * S =:= N.

generate_n1(Limit, P, S, K, N) :-
    P2 is P * P,
    S2 is S * S,
    K2 is K * K,
    N is K2 * S * P2 * P + K * S2,
    N < Limit,
    is_square(N).

generate_n2(Limit, P, S, K, N) :-
    P2 is P * P,
    S2 is S * S,
    SP is S * P,
    K2 is K * K,
    N is K2 * S2 * P2 + K * SP,
    N < Limit,
    is_square(N).

generate_n(Limit, N) :-
    between(2, 5000, P),        
    between(1, P, S),
    S < P,
    gcd(P, S, 1),
    between(1, 1000000, K),     % k
    ( generate_n1(Limit, P, S, K, N)
    ; generate_n2(Limit, P, S, K, N)
    ).

sum_progressive_squares(Limit, Sum) :-
    findall(N, generate_n(Limit, N), List),
    sort(List, Unique),
    sum_list(Unique, Sum).

main :-
    Limit = 1000000000000,  % 10^12
    sum_progressive_squares(Limit, Sum),
    write('Sum of progressive perfect squares below 10^12: '),
    write(Sum), nl.