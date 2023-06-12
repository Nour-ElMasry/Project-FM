const CountryItem = (props) => {
  const displayFlag = () => {
    var country = props.country.toLowerCase();
    if (country.includes("republic")) {
      country = country.replace("republic", "");
      country = country.replace("of", "");
      country = country.replace(/\s/g, "");
    }

    if (country.includes("england")) {
      country = "gb";
    }

    if (country.includes("d'Ivoire")) {
      country = "civ";
    }

    if (country.includes("sweden")) {
      country = "se";
    }

    if (country.includes("spain")) {
      country = "es";
    }

    if (country.includes("portugal")) {
      country = "pt";
    }

    if (country.includes("wales")) {
      country = "gb";
    }

    if (country.includes("rkiye")) {
      country = "tr";
    }

    if (country.includes("ukraine")) {
      country = "ua";
    }

    if (country.includes("switzerland")) {
      country = "ch";
    }

    if (country.includes("poland")) {
      country = "pl";
    }

    if (country.includes("angola")) {
      country = "ao";
    }

    if (country.includes("korea")) {
      country = "kr";
    }

    if (country.includes("zimbabwe")) {
      country = "zw";
    }

    if (country.includes("japan")) {
      country = "jp";
    }

    if (country.includes("uruguay")) {
      country = "uy";
    }

    if (country.includes("côte d'ivoire")) {
      country = "civ";
    }

    if (country.includes("yugoslavia")) {
      return (
        <img
          src="https://upload.wikimedia.org/wikipedia/commons/thumb/6/61/Flag_of_Yugoslavia_%281946-1992%29.svg/800px-Flag_of_Yugoslavia_%281946-1992%29.svg.png?20220813004708"
          alt={props.country}
        />
      );
    }

    return (
      <img
        crossOrigin="anonymous"
        src={
          "https://flagcdn.com/48x36/" +
          country.substring(0, 2).toLowerCase() +
          ".png"
        }
        alt={props.country}
      />
    );
  };
  return <>{displayFlag()}</>;
};

export default CountryItem;
