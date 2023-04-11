import { IGetProductsQuery } from "../models/RequestModels";
import { IProduct, IResponse } from "../models/ResponseModels";
import { fetchWrapper } from "./FetchWrapper";

const rootUrl = "https://localhost:7261/product";

export const ProductsService = {
  getProducts,
};

export async function getProducts(
  query: IGetProductsQuery
): Promise<IResponse<IProduct[]>> {
  const url = new URL(rootUrl);

  if (query.searchString && query.searchString !== "") {
    url.searchParams.append("searchString", query.searchString);
  }
  if (query.pageNumber) {
    url.searchParams.append("pageNumber", query.pageNumber.toString());
  }
  if (query.pageSize) {
    url.searchParams.append("pageSize", query.pageSize.toString());
  }
  if (query.sortColumn) {
    url.searchParams.append("sortColumn", query.sortColumn);
  }
  if (query.sortAscending) {
    url.searchParams.append("sortAscending", query.sortAscending.toString());
  }

  const response = await fetchWrapper.get<IProduct[]>(url.toString());

  return response;
}
