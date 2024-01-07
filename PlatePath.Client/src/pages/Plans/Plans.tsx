import { Typography } from "@mui/material";
import PageWrapper from "../../components/PageWrapper";
import { Columns } from "../../components";

import PlansForm from "./PlansForm";
import PlansList from "./PlansList";
export type Plan = {
  name: string;
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
  return (
    <>
      <PageWrapper title="Add New Plan">
        <Columns gap="30px">
          <Typography variant="h5">Submit New Plan</Typography>
          <PlansForm />
        </Columns>
      </PageWrapper>
      <PageWrapper>
        <Columns gap="30px" width="100%">
          <Typography variant="h5">Plans</Typography>
          <PlansList />
        </Columns>
      </PageWrapper>
    </>
  );
};
export default Plans;
