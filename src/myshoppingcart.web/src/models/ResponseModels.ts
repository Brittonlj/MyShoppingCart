export interface IResponse<T> {
  data?: T;
  error?: string;
}

export interface IProduct {
  id: number;
  name: string;
  description: string;
  price: number;
  imageUrl?: string;
}
