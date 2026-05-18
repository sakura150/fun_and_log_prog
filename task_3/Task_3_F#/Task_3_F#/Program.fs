open System
open System.Diagnostics

let gcd a b =
    let rec gcd' a b =
        match b with
        | 0L -> abs a
        | _ -> gcd' b (a % b)
    gcd' a b

let solve maxPerimeter =
    let maxM = int64 (sqrt (float maxPerimeter / 2.0)) + 1L
    
    let generateScaledTriangles a0 b0 c0 =
        let rec generate k acc =
            let a = a0 * k
            let b = b0 * k
            let c = c0 * k
            let perimeter = a + b + c
            match perimeter > maxPerimeter with
            | true -> acc
            | false -> generate (k + 1L) (1L :: acc)
        generate 1L []
    
    let isPrimitiveValid m n =
        match (m - n) % 2L <> 0L && gcd m n = 1L with
        | true ->
            let a0 = m * m - n * n
            let b0 = 2L * m * n
            let c0 = m * m + n * n
            let diff = abs (a0 - b0)
            match c0 % diff with
            | 0L -> Some (a0, b0, c0)
            | _ -> None
        | false -> None
    
    let countTrianglesForPair m n =
        match isPrimitiveValid m n with
        | Some (a0, b0, c0) -> 
            let perimeter0 = a0 + b0 + c0
            maxPerimeter / perimeter0
        | None -> 0
    
    let allPairs =
        [2L..maxM]
        |> List.collect (fun m ->
            [1L..m-1L]
            |> List.map (fun n -> (m, n)))
    
    allPairs
    |> List.sumBy (fun (m, n) -> countTrianglesForPair m n)

[<EntryPoint>]
let main argv =
    let maxPerimeter = 
        match argv with
        | [|arg|] -> Int64.Parse arg
        | _ -> 100_000_000L
    
    use currentProcess = Process.GetCurrentProcess()
    currentProcess.Refresh()
    
    let sw = Stopwatch.StartNew()
    
    let result = solve maxPerimeter
    
    sw.Stop()
    
    let totalMemory = currentProcess.WorkingSet64
    
    printfn "Общее потребление памяти: ( %.2f / 1024.0 / 1024.0) МБ" (float totalMemory) 
    printfn "Время выполнения: %.2f мс" sw.Elapsed.TotalMilliseconds 
    printfn "Всего найдено треугольников: %d" result
    
    0