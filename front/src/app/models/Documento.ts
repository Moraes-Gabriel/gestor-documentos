import { TimestampProvider } from "rxjs";
import { Concessao } from "./Concessao";
import { User } from "./identity/User";
import { Usuario } from "./identity/usuario";
import { Tipo } from "./Tipo";

export class Documento {
    id: number | any;
    nome!: string;
    descricao!: string;
    abreviacao!: string;
    urlArquivoS3!: string;
    editadoOn!: Date;
    data!: string;
    usuario!:Usuario;
    concessao!: Concessao;
    tipo!: Tipo;
}