import { Typography } from "@mui/material";
import PageWrapper from "../../components/PageWrapper";
import { Columns } from "../../components";

import PlansForm from "./PlansForm";
import PlansList from "./PlansList";
import { apiUrl, useAuth } from "../../components/auth";
import { useState } from "react";
export type Plan = {
  mealPlanName: string;
  days: number;
  mealsPerDay: number;
  minCalories: number;
  maxCalories: number;
  proteins: number;
  fats: number;
  carbohydrates: number;
  dietType: string;
};
const Plans = () => {
  const { getToken } = useAuth();
  const [names, setNames] = useState([]);

  const getNames = () => {
    const token = getToken();
    const myHeaders = new Headers({
      "Content-Type": "application/json",
      Authorization: `Bearer ${token}`,
    });
    fetch(`${apiUrl}/MealPlans/getAll`, {
      method: "GET",
      headers: myHeaders,
    })
      .then((r) => r.json())
      .then((res) => {
        if (res.mealPlanNames) {
          setNames(res.mealPlanNames);
        }
      })
      .catch((err) => alert(err));
  };
  return (
    <>
      <PageWrapper title="Add New Plan">
        <Columns gap="30px">
          <Typography variant="h5">Submit New Plan</Typography>
          <PlansForm getNames={getNames} />
        </Columns>
      </PageWrapper>
      <PageWrapper>
        <Columns gap="30px" width="100%">
          <Typography variant="h5">Meal Plans</Typography>
          <PlansList getNames={getNames} names={names} />
        </Columns>
      </PageWrapper>
    </>
  );
};
export default Plans;
