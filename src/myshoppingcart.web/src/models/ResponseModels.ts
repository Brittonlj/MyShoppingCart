import { UUID } from "crypto";

export interface IResponse<T> {
  data?: T;
  error?: string;
}

export interface IProduct {
  id: UUID;
  name: string;
  description: string;
  price: number;
  imageUrl?: string;
  categories: ICategory[];
}

export interface ICategory {
  id: UUID;
  name: string;
}
