import { Typography, styled } from "@mui/material";
import { BoxContainer, Columns } from "../../components";
import PageWrapper from "../../components/PageWrapper";
import defaultAvatar from "../../assets/defaultAvatar.png";
import ProfileEditForm from "../../components/ProfileEditForm";
const ProfileInfoWrapper = styled(BoxContainer)`
  width: 100%;
  text-align: center;
  & .avatar-wrapper {
    position: relative;
    height: fit-content;
    padding-bottom: 10px;
    &::after {
      content: " ";
      position: absolute;
      left: 50%;
      bottom: 0%;
      transform: translate(-50%, -50%);
      height: 3px;
      width: 30%;
      background: ${({ theme }) => theme.colors.green};
      border-radius: 6px;
    }
  }
  & .avatar {
    width: 140px;
    height: 140px;
    border-radius: 4px;
    margin-bottom: 5px;
  }
  & .content-wrapper {
    align-items: center;
    gap: 50px;
    width: 100%;
  }
  & .info-wrapper {
    align-items: flex-start;
    width: 100%;
  }
`;
interface InfoLabelProps {
  label: string;
  data: string;
}
const InfoLabel = ({ label, data }: InfoLabelProps) => {
  return (
    <BoxContainer>
      <Typography mr="30px" variant="subtitle">
        {label}:
      </Typography>
      <Typography variant="title20" sx={{ fontSize: "14px" }}>
        {data}
      </Typography>
    </BoxContainer>
  );
};
const Profile = () => {
  return (
    <>
      <PageWrapper title="User Personal Information">
        <ProfileInfoWrapper>
          <Columns className="content-wrapper">
            <Columns gap="5px" className="avatar-wrapper">
              <img className="avatar" src={defaultAvatar} alt="avatar"></img>
              <Typography variant="subtitle1" sx={{ fontSize: "20px" }}>
                John Lane
              </Typography>
              <Typography variant="subtitle" sx={{ fontSize: "14px" }}>
                johnlane123
              </Typography>
            </Columns>
            <Columns gap="30px" className="info-wrapper">
              <InfoLabel label="Email" data="johnlane123@gmail.com" />
              <InfoLabel label="Phone" data="+1 123 456 7890" />
              <InfoLabel label="Gender" data="Male" />
              <InfoLabel label="Date of Birth" data="June 21, 1989" />
              <InfoLabel
                label="Location"
                data="4664 Buena Vista Avenue Eugene, OR 97401"
              />
            </Columns>
          </Columns>
        </ProfileInfoWrapper>
      </PageWrapper>
      <PageWrapper><ProfileEditForm/></PageWrapper>
    </>
  );
};
export default Profile;
