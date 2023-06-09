import { useEffect, useRef, useState } from "react";
import {
  Badge,
  Button,
  Card,
  Col,
  Container,
  Form,
  InputGroup,
  Row,
} from "react-bootstrap";
import { IGetProductsQuery } from "../models/RequestModels";
import { IProduct } from "../models/ResponseModels";
import { ProductsService } from "../services/ProductsService";
import InfiniteScroll from "react-infinite-scroll-component";

export default function Shop() {
  const searchString = useRef<HTMLInputElement | null>(null);

  const defaultProductsQuery: IGetProductsQuery = {
    searchString: undefined,
    pageNumber: 1,
    pageSize: 5,
    sortColumn: "Name",
    sortAscending: true,
  };

  const [hasMore, setHasMore] = useState<boolean>(true);
  const [products, setProducts] = useState<IProduct[]>([]);
  const [productsQuery, setProductsQuery] =
    useState<IGetProductsQuery>(defaultProductsQuery);

  async function loadProducts() {
    try {
      const productsResponse = await ProductsService.getProducts(productsQuery);
      if (productsResponse.error) {
        console.log(productsResponse.error);
        return;
      }

      if (!productsResponse.data || productsResponse.data.length === 0) {
        setHasMore(false);
        return;
      }

      const data = productsResponse.data as IProduct[];

      setProducts((p) => {
        if (
          !data.every((newProduct) =>
            p.some((existingProduct) => existingProduct.id === newProduct.id)
          )
        ) {
          return p.concat(data);
        }
        return [...p];
      });
    } catch (ex) {
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
      <Container className="d-flex">
        <InfiniteScroll
          dataLength={products.length}
          next={loadNextPage}
          hasMore={hasMore}
          loader={<h4>Loading...</h4>}
          endMessage={
            <p style={{ textAlign: "center" }}>
              <b>No more products were found.</b>
            </p>
          }
        >
          <Row xxs={1} sm={2} md={3} lg={4}>
            {products.map((x) => (
              <Col key={x.id}>
                <Card className="lg-3 mb-3">
                  <Card.Img
                    variant="top"
                    src={x.imageUrl ?? "/img/default-image.png"}
                    className="p-3"
                  />
                  <Card.Body>
                    <Card.Title>{x.name}</Card.Title>
                    <Card.Text>{x.description}</Card.Text>
                    <Container>
                      {x.categories.map((y) => (
                        <Badge key={y.id} bg="secondary" className="m-1">
                          {y.name}
                        </Badge>
                      ))}
                    </Container>
                    <Button variant="primary">Add to cart</Button>
                  </Card.Body>
                </Card>
              </Col>
            ))}
          </Row>
        </InfiniteScroll>
      </Container>
    </>
  );
}
