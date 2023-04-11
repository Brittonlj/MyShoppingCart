import { IResponse } from "../models/ResponseModels";

export const fetchWrapper = {
  get,
  post,
  put,
  delete: _delete,
};

async function get<T>(url: string): Promise<IResponse<T>> {
  const requestOptions = {
    method: "GET",
    header: {
      "Access-Control-Allow-Origin": "https://localhost:7261",
    },
  };
  var response = await fetch(url, requestOptions);
  return handleResponse(response);
}

async function post<T>(url: string, body: any): Promise<IResponse<T>> {
  const requestOptions = {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(body),
  };
  var response = await fetch(url, requestOptions);
  return handleResponse(response);
}

async function put<T>(url: string, body: any): Promise<IResponse<T>> {
  const requestOptions = {
    method: "PUT",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(body),
  };
  var response = await fetch(url, requestOptions);
  return handleResponse(response);
}

// prefixed with underscored because delete is a reserved word in javascript
async function _delete(url: string) {
  const requestOptions = {
    method: "DELETE",
  };
  var response = await fetch(url, requestOptions);
  return handleResponse(response);
}

// helper functions

async function handleResponse<T>(response: Response): Promise<IResponse<T>> {
  if (response.ok && response.status === 200) {
    return {
      data: await response.json(),
    };
  }

  const data = await response.text();
  const error = (data && data) || response.statusText;
  return Promise.reject({
    error: error,
  });
}
