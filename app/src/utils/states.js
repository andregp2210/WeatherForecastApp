import rawCountryStates from "iso3166-2-db/i18n/en";


export const STATES_LIST = rawCountryStates["US"].regions
  .map((state) => ({
    label: state.name,
    value: state.iso,
  }))
  .sort((a, b) => (a.label > b.label ? 1 : -1));
