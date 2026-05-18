let rec gcd a b = if b = 0L then a else gcd b (a % b)

let isSquare n =
    let s = int64 (sqrt (float n))
    s * s = n

let rec generateFromFormula1 limit p s k acc =
    let p2 = p * p
    let s2 = s * s
    let n = k * k * s * p2 * p + k * s2
    if n >= limit then acc
    elif isSquare n then
        generateFromFormula1 limit p s (k + 1L) (n :: acc)
    else
        generateFromFormula1 limit p s (k + 1L) acc

let rec generateFromFormula2 limit p s k acc =
    let p2 = p * p
    let s2 = s * s
    let sp = s * p
    let n = k * k * s2 * p2 + k * sp
    if n >= limit then acc
    elif isSquare n then
        generateFromFormula2 limit p s (k + 1L) (n :: acc)
    else
        generateFromFormula2 limit p s (k + 1L) acc

let rec generateForP limit p s acc =
    if s >= p then acc
    elif gcd p s <> 1L then generateForP limit p (s + 1L) acc
    else
        let from1 = generateFromFormula1 limit p s 1L []
        let from2 = generateFromFormula2 limit p s 1L []
        let newAcc = List.concat [acc; from1; from2]
        generateForP limit p (s + 1L) newAcc

let rec generateAll limit p acc =
    let p2 = p * p
    let minN = p2 * p2  
    if minN >= limit then acc
    else
        let newAcc = generateForP limit p 1L acc
        generateAll limit (p + 1L) newAcc

let sumProgressiveSquares limit =
    generateAll limit 2L []
    |> List.distinct
    |> List.sum

[<EntryPoint>]
let main argv =
    let limit = 1000000000000L 
    let result = sumProgressiveSquares limit
    printfn "Sum of progressive perfect squares below 10^12: %d" result
    0