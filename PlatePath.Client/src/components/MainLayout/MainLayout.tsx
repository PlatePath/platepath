import { Link } from "react-router-dom";
import { BoxContainer, Columns } from "../styled";
import { styled, Button } from "@mui/material";
interface MainLayoutProps {
  children: React.ReactNode;
}
const SideNav = styled(Columns)`
  width: 310px;
  height: 100%;
  padding: 24px 28px;
  align-items: center;
  background: white;
  display: inline-block;
  -webkit-box-shadow: 1px 0px 13px -7px rgba(0, 0, 0, 0.25);
  -moz-box-shadow: 1px 0px 13px -7px rgba(0, 0, 0, 0.25);
  box-shadow: 1px 0px 13px -7px rgba(0, 0, 0, 0.25);
  position: relative;
`;

const MainLayout = ({ children }: MainLayoutProps) => {
  return (
    <BoxContainer
      sx={{
        justifyContent: "flex-start",
        height: "100%",
      }}
    >
      <SideNav>
        <p>title</p>
        <Columns gap="20px">
          <Button>Dashboard</Button>
          <Button>Your Profile</Button>
          <Button>Recipes</Button>
          <Button>Forum</Button>
          <Button>
            <Link to="/plans">Plans</Link>
          </Button>
          <Button>FAQ</Button>
        </Columns>
      </SideNav>
      {children}
    </BoxContainer>
  );
};
export default MainLayout;
