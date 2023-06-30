const RatingSpan = (props) => {
  var classNames = "playerRating";

  if (props.rating < 60)
    return (
      <span
        className={
          props.overall
            ? classNames + " playerRating__bronze"
            : classNames + " playerRating__red"
        }
      >
        {props.rating}
      </span>
    );
  else if (props.rating < 80)
    return (
      <span
        className={
          props.overall
            ? classNames + " playerRating__silver"
            : classNames + " playerRating__yellow"
        }
      >
        {props.rating}
      </span>
    );

  return (
    <span
      className={
        props.overall
          ? classNames + " playerRating__gold"
          : classNames + " playerRating__green"
      }
    >
      {props.rating}
    </span>
  );
};

export default RatingSpan;
