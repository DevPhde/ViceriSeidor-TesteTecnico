import { Superpoder } from "./Superpoder.interface";

export interface Heroi {
    id: number,
    nome: string,
    nomeHeroi: string,
    dataNascimento: Date,
    altura: number,
    peso: number,
    superpoderes: Superpoder[]
}