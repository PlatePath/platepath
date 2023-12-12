import { Typography, styled } from "@mui/material";
import { BoxContainer } from "./styled";

const NavbarContent = styled(BoxContainer)`
  height: 90px;
  background: white;
  padding: 20px;
  width: 100%;
  justify-content: flex-start;
  align-items: center;
`;
export interface NavbarProps {
  title: string;
}
const Navbar = ({ title }: NavbarProps) => {
  return (
    <NavbarContent>
      <Typography fontSize="20px">{title}</Typography>
    </NavbarContent>
  );
};
export default Navbar;
