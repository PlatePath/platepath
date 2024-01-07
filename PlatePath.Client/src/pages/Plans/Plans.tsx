import { Alert, Button, TextField } from "@mui/material";
import PageWrapper from "../../components/PageWrapper";
import { BoxContainer, Columns } from "../../components";
import { useState } from "react";

const Plans = () => {
  const [jsonData, setJsonData] = useState({
    days: 5,
    mealsPerDay: 3,
    minCalories: 1800,
    maxCalories: 3000,
  });
  let currentTimeout;
  const [showAlert, setShowAlert] = useState(false);
  const onSubmit = () => {
    currentTimeout = null;
    setShowAlert(true);
    currentTimeout = setTimeout(() => {
      setShowAlert(false);
    }, 5000);

    // fetch("http://localhost:7010/api/MealPlan/generate", {
    //   method: "POST",
    //   mode: "no-cors",
    //   body: JSON.stringify(jsonData), // body data type must match "Content-Type" header
    // })
    //   .then((res) => {
    //     console.log(res);
    //   })
    //   .catch((e) => {
    //     console.log(e);
    //   });
  };
  const handleInputChange = (
    event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>,
    id: string
  ) => {
    const target = event.target as HTMLInputElement;
    if (target.value.match(/^[0-9]+$/))
      setJsonData({ ...jsonData, [id]: target.value });
  };
  return (
    <PageWrapper title="Add New Plan">
      <Columns gap="20px">
        {showAlert ? (
          <Alert severity="success">Plan submitted successfully</Alert>
        ) : null}
        <TextField
          label="Days"
          value={jsonData.days}
          onChange={(e) => handleInputChange(e, "days")}
        />
        <TextField
          label="Meals per day"
          value={jsonData.mealsPerDay}
          onChange={(e) => handleInputChange(e, "mealsPerDay")}
        />
        <BoxContainer gap="10px">
          <TextField
            label="Min Calories"
            value={jsonData.minCalories}
            onChange={(e) => handleInputChange(e, "minCalories")}
          />
          <TextField
            label="Max Calories"
            value={jsonData.maxCalories}
            onChange={(e) => handleInputChange(e, "maxCalories")}
          />
        </BoxContainer>
        <Button variant="contained" onClick={onSubmit}>
          Submit Plan
        </Button>
      </Columns>
    </PageWrapper>
  );
};
export default Plans;
