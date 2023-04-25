import { useQuery } from "react-query";
import { apiBaseUrl } from "./constants";
import { DataDiff } from "./models";

export const apiGetDataDiff = async () => {
  const response = await fetch(apiBaseUrl + "/latest-diff");
  const data = await response.json();
  return data as DataDiff;
};

export const useDataDiff = () => useQuery("dataDiff", apiGetDataDiff);
