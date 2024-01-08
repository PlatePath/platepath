import { Link, LinkProps, useNavigate } from "react-router-dom";
import { BoxContainer, Columns } from "../styled";
import defaultAvatar from "../../assets/defaultAvatar.png";
import logo128 from "../../assets/logo128.png";

import {
  styled,
  Button,
  Avatar,
  Typography,
  avatarClasses,
  ButtonProps,
  Box,
} from "@mui/material";
import { ReactNode, useEffect } from "react";
import { useAuth } from "../auth";
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
  & .side-link {
    width: 100%;
    & > button {
      width: inherit;
    }
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
interface SideLinkProps extends Pick<LinkProps, "to"> {
  children: ReactNode;
  disabled?: ButtonProps["disabled"];
}
const SideLink = ({ to, children, disabled }: SideLinkProps) => {
  const Parent = disabled ? Box : Link;
  const props = disabled
    ? { className: "side-link" }
    : ({ to: to, className: "side-link" } as any);
  return (
    <Parent {...props}>
      <Button variant="text" children={children} disabled={disabled} />
    </Parent>
  );
};

const MainLayout = ({ children }: MainLayoutProps) => {
  const { isLogged, setToken } = useAuth();
  const navigate = useNavigate();
  useEffect(() => {
    if (!isLogged()) {
      navigate("/login");
    }
  });
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
            <SideLink to="/dashboard" disabled>
              Dashboard
            </SideLink>
            <SideLink to="/profile">Your Profile</SideLink>
            <SideLink to="/recipes" disabled>
              Recipes
            </SideLink>
            <SideLink to="/forum" disabled>
              Forum
            </SideLink>
            <SideLink to="/plans">Plans</SideLink>
            <SideLink to="/faq" disabled>
              FAQ
            </SideLink>
          </Columns>
          <Button
            variant="text"
            onClick={() => setToken("")}
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
