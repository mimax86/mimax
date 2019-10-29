let parse(text:string) = 
    let data = text.Split()
    let playername = data.[0];
    let game = data.[1]
    let score = int(data.[2])
    playername, game, score

let result = parse("Mary Asteroids 2500")
let a,b,c= result