export interface IGetProductsQuery {
  searchString?: string;
  pageNumber: number;
  pageSize: number;
  sortColumn: string;
  sortAscending: boolean;
}
