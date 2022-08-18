import { Enumeration } from "./enumeration";

export class Game{
  name!: string;
  summary!: string;
  developer!: string;
  gameGenderId?: string;
  gameGender!: Enumeration;
  score!: number;
  console!: string;
}
