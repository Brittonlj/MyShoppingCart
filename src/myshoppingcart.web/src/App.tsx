import React from "react";
import "./App.scss";
import { ThemeProvider } from "react-bootstrap";
import Heading from "./components/Heading";
import { Route, Routes } from "react-router-dom";
import Shop from "./components/Shop";
import Login from "./components/Login";
import Cart from "./components/Cart";

function App() {
  return (
    <>
      <ThemeProvider
        breakpoints={["xxxl", "xxl", "xl", "lg", "md", "sm", "xs", "xxs"]}
        minBreakpoint="xxs"
      >
        <div className="App">
          <Heading></Heading>
          <Routes>
            <Route path="/" element={<Shop />} />
            <Route path="/cart" element={<Cart />} />
            <Route path="/login" element={<Login />} />
          </Routes>
        </div>
      </ThemeProvider>
    </>
  );
}

export default App;
