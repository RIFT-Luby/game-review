import { Enumeration } from "./enumeration";
import { Register } from "./register";

export class Game extends Register {
  name!: string;
  summary!: string;
  developer!: string;
  gameGenderId?: string;
  gameGender!: Enumeration;
  score!: number;
  console!: string;
}
