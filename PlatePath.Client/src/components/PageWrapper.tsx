import { styled } from "@mui/material";

import { ReactNode } from "react";
import { BoxContainer, Columns } from "./styled";
import Navbar, { NavbarProps } from "./Navbar";

const PageWrapperContent = styled(BoxContainer)`
  margin: 50px;
  border-radius: 20px;
  flex: 1;
  padding: 70px;
  justify-content: flex-start;
  background: white;
  height: 80%;
`;
interface PageWrapperProps extends NavbarProps {
  children: ReactNode;
}
const PageWrapper = ({ title, children }: PageWrapperProps) => {
  return (
    <Columns
      sx={{
        width: "100%",
        height: "100vh",
      }}
    >
      <Navbar title={title} />
      <PageWrapperContent>{children}</PageWrapperContent>
    </Columns>
  );
};
export default PageWrapper;
