import { useEffect, useRef, useState } from "react";
import { Button, Container, Form, InputGroup } from "react-bootstrap";
import { IGetProductsQuery } from "../models/RequestModels";
import { IProduct } from "../models/ResponseModels";
import { ProductsService } from "../services/ProductsService";

export default function Shop() {
  const searchString = useRef<HTMLInputElement | null>(null);

  const defaultProductsQuery: IGetProductsQuery = {
    searchString: undefined,
    pageNumber: 1,
    pageSize: 5,
    sortColumn: "Name",
    sortAscending: true,
  };

  const [products, setProducts] = useState<IProduct[]>([]);
  const [productsQuery, setProductsQuery] =
    useState<IGetProductsQuery>(defaultProductsQuery);

  async function loadProducts() {
    try {
      const productsResponse = await ProductsService.getProducts(productsQuery);
      if (productsResponse.error) {
        // setProducts([]);
        console.log(productsResponse.error);
        return;
      }

      if (!productsResponse.data) {
        return;
      }

      const data = productsResponse.data as IProduct[];

      setProducts((p) => {
        const setOfData = new Set(data);
        if (p.every((product) => setOfData.has(product))) {
          return p.concat(data);
        }
        return [...p];
      });
    } catch (ex) {
      // setProducts([]);
      console.log(ex);
    }
  }

  function loadNextPage() {
    setProductsQuery((o) => {
      const query = {
        ...o,
      };
      query.pageNumber = o.pageNumber + 1;
      return query;
    });
  }

  function handleSearchButtonClick(e: React.MouseEvent<HTMLButtonElement>) {
    const query = {
      ...defaultProductsQuery,
    };
    query.searchString = searchString?.current?.value;
    setProducts([]);
    setProductsQuery(query);
  }

  useEffect(() => {
    loadProducts();
  }, [productsQuery]);

  return (
    <>
      <Container className="pt-4">
        <Form>
          <Form.Group className="pb-3 d-flex" controlId="search">
            <InputGroup>
              <Form.Control
                type="text"
                placeholder="Search products..."
                ref={searchString}
              />
              <Button
                variant="primary"
                onClick={(e) => handleSearchButtonClick(e)}
              >
                Search
              </Button>
            </InputGroup>
          </Form.Group>
        </Form>
      </Container>
      <Container>
        {products && products.map((x) => <p key={x.id}>{x.name}</p>)}
      </Container>
    </>
  );
}
