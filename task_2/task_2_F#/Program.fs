
let rec gcd (a: int64) (b: int64) =
    if b = 0L then a else gcd b (a % b)

let rec factorialBig (n: int64) : bigint =
    let rec loop acc = function
        | 0L -> acc
        | n -> loop (acc * bigint n) (n - 1L)
    loop 1I n

let f (m: int) (n: int64) =
    let N = int64 m * n
    let rec sumK k acc =
        if k >= N then acc
        else
            let g = gcd N k
            let cycleLen = N / g
            if n % cycleLen <> 0L then sumK (k + 1L) acc
            else
                let colorCycles = n / cycleLen
                if int64 m * colorCycles <> g then sumK (k + 1L) acc
                else
                    let term = (factorialBig g) / (pown (factorialBig colorCycles) m)
                    sumK (k + 1L) (acc + term)
    (sumK 0L 0I) / bigint N

let rec collectForM (m: int) (n: int64) (limit: int64) (acc: int64) =
    let valMN = f m n
    if valMN > bigint limit then acc
    else
        collectForM m (n + 1L) limit (acc + int64 valMN)

let rec collectAll (m: int) (limit: int64) (acc: int64) =
    let first = collectForM m 1L limit 0L
    if first = 0L then acc
    else
        collectAll (m + 1) limit (acc + first)

let sumAllF (limit: int64) =
    collectAll 2 limit 0L

[<EntryPoint>]
let main argv =
    let limit = 1000000000000000L 
    let result = sumAllF limit
    printfn "Sum of all f(m,n) <= 10^15: %d" result
    0