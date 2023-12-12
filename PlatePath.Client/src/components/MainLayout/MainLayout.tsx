import { Link } from "react-router-dom";
import { BoxContainer, Columns } from "../styled";
import defaultAvatar from "../../assets/defaultAvatar.png";
import logo128 from "../../assets/logo128.png";

import {
  styled,
  Button,
  Avatar,
  Typography,
  avatarClasses,
} from "@mui/material";
interface MainLayoutProps {
  children: React.ReactNode;
}
const SideNav = styled(Columns)`
  width: 310px;
  height: 100%;
  padding: 24px 28px;
  align-items: center;
  background: white;
  -webkit-box-shadow: 1px 0px 13px -7px rgba(0, 0, 0, 0.25);
  -moz-box-shadow: 1px 0px 13px -7px rgba(0, 0, 0, 0.25);
  box-shadow: 1px 0px 13px -7px rgba(0, 0, 0, 0.25);
  position: relative;
  & .sidebar-logo {
    position: absolute;
    left: 50%;
    top: 80%;
    transform: translate(-50%, -50%);
    opacity: 0.3;
    width: 70%;
  }
  & .button-group {
    width: 100%;
    justify-content: space-between;
    flex: 1;
  }
`;
const ProfileInfo = styled(Columns)`
  gap: 7px;
  align-items: center;
  width: 100%;
  margin-bottom: 80px;
  & .${avatarClasses.root} {
    width: 100px;
    height: 100px;
  }
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
        <img className="sidebar-logo" src={logo128} alt="logo"></img>
        <ProfileInfo>
          <Avatar src={defaultAvatar} />
          <Typography variant="title20">Hello! Michael!</Typography>
          <Typography variant="subtitle">michaelwilson12@gmail.com</Typography>
        </ProfileInfo>
        <Columns className="button-group">
          <Columns gap="20px">
            <Button variant="text">Dashboard</Button>
            <Button variant="text">Your Profile</Button>
            <Button variant="text">Recipes</Button>
            <Button variant="text">Forum</Button>
            <Button variant="text">
              <Link to="/plans">Plans</Link>
            </Button>
            <Button>FAQ</Button>
          </Columns>
          <Button
            variant="text"
            sx={{
              background: "lightgrey",
              fontWeight: 600,
              color: "#F6F6F6 !important",
            }}
          >
            Log out
          </Button>
        </Columns>
      </SideNav>
      {children}
    </BoxContainer>
  );
};
export default MainLayout;
