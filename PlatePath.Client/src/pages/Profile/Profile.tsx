import { Alert, Divider, Typography, styled } from "@mui/material";
import { BoxContainer, Columns } from "../../components";
import PageWrapper from "../../components/PageWrapper";
import defaultAvatar from "../../assets/defaultAvatar.png";
import ProfileEditForm from "../../components/ProfileEditForm";
import { useEffect, useState } from "react";
import { apiUrl, useAuth } from "../../components/auth";
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
    text-align: start;
  }
`;
interface InfoLabelProps {
  label: string;
  data: string;
}
const InfoLabel = ({ label, data }: InfoLabelProps) => {
  return (
    <BoxContainer>
      <Typography mr="20px" variant="subtitle">
        {label}:
      </Typography>
      <Typography variant="title20" sx={{ fontSize: "14px" }}>
        {data}
      </Typography>
    </BoxContainer>
  );
};
export type ProfileData = {
  age: number | string;
  heightCm: number | string;
  weightKg: number | string;
  gender: "male" | "female";
  activityLevel: string;
  weightGoal: string;
};
export type NeededNutrition = {
  calories: number;
  proteinGrams: number;
  fatGrams: number;
  carbGrams: number;
};
const Profile = () => {
  const [personalData, setPersonalData] = useState<ProfileData>();
  const [neededNutrition, setNeededNutrition] = useState<NeededNutrition>();
  const { getToken } = useAuth();
  const fetchData = (data?: ProfileData) => {
    const token = getToken();
    const myHeaders = new Headers({
      "Content-Type": "application/json",
      Authorization: `Bearer ${token}`,
    });
    fetch(`${apiUrl}/User/${data ? "set" : "get"}PersonalData`, {
      method: "POST",
      body: JSON.stringify(data),
      headers: myHeaders,
    })
      .then((r) => r.json())
      .then((res) => {
        console.log(res);
        if (!res.age) {
          fetchData();
        }
        if (res.age) {
          setPersonalData(res);
        }
      })
      .catch((err) => alert(err));
  };
  useEffect(() => {
    const token = getToken();
    fetchData();
    const myHeaders = new Headers({
      "Content-Type": "application/json",
      Authorization: `Bearer ${token}`,
    });
    fetch(`${apiUrl}/User/getNeededNutrition`, {
      method: "GET",
      headers: myHeaders,
    })
      .then((r) => r.json())
      .then((res) => {
        if (res.calories) {
          setNeededNutrition(res);
        } else {
          setNeededNutrition(undefined);
        }
      })
      .catch((err) => alert(err));
  }, []);
  return (
    <>
      <PageWrapper title="User Personal Information">
        <ProfileInfoWrapper>
          <Columns className="content-wrapper">
            {personalData ? (
              <>
                <Columns gap="5px" className="avatar-wrapper" mb="30px">
                  <img
                    className="avatar"
                    src={defaultAvatar}
                    alt="avatar"
                  ></img>
                  <Typography variant="subtitle1" sx={{ fontSize: "20px" }}>
                    John Lane
                  </Typography>
                </Columns>
                <Columns gap="30px" className="info-wrapper">
                  <InfoLabel label="Age" data={`${personalData.age} years`} />
                  <InfoLabel
                    label="Height"
                    data={`${personalData.heightCm} cm`}
                  />
                  <InfoLabel
                    label="Weight"
                    data={`${personalData.weightKg} kg`}
                  />
                  <InfoLabel label="Gender" data={personalData.gender} />
                  <InfoLabel
                    label="Weight Goal"
                    data={`${personalData?.weightGoal}`}
                  />
                  <InfoLabel
                    label="Activity Level"
                    data={personalData?.activityLevel}
                  />
                </Columns>
                {neededNutrition ? (
                  <Columns className="info-wrapper">
                    <Columns>
                      <Typography variant="h5">Needed Nutrition</Typography>
                      <Divider />
                    </Columns>
                    <Columns gap="5px" mt="15px" className="info-wrapper">
                      <InfoLabel
                        label="Calories"
                        data={`${neededNutrition?.calories}kcal`}
                      />
                      <InfoLabel
                        label="Fats"
                        data={`${neededNutrition?.fatGrams}g`}
                      />
                      <InfoLabel
                        label="Carbohydrates"
                        data={`${neededNutrition?.carbGrams}g`}
                      />
                      <InfoLabel
                        label="Protein"
                        data={`${neededNutrition?.proteinGrams}g`}
                      />
                    </Columns>
                  </Columns>
                ) : null}
              </>
            ) : (
              <Alert severity="error">
                No Personal Information Found <br />
                Please fill out the form
              </Alert>
            )}
          </Columns>
        </ProfileInfoWrapper>
      </PageWrapper>
      <PageWrapper>
        <ProfileEditForm setData={fetchData} />
      </PageWrapper>
    </>
  );
};
export default Profile;
