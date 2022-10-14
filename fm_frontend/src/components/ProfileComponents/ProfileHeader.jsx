import DateService from "../../services/DateService";
import CountryItem from "../CountryItem";

const ProfileHeader = (props) => {
    return <div className="profileHeader container--styled">
        <div className="globalHeaderInfo">
            <img className="globalImg" src={props.user.userPerson.image} alt="user"></img>
            <div className="globalName">
                <h2>{props.user?.userPerson?.name}</h2>
                <em>{props.user?.userUserName}</em>
            </div>
        </div>
        <div className="globalHeaderInfo flex-jc-sa">
            <div>
                <h4>
                    {DateService.dateLongFormat(props.user?.userPerson?.birthDate)}
                </h4>
                <p>Date Of Birth</p>
                <br/>
                <h4>
                    {DateService.getAge(props.user?.userPerson?.birthDate)}
                </h4>
                <p>Age</p>
            </div>

            <div className="countryContainer flex flex-ai-c">
                <h4>Country</h4>
                <span className="countryLogo">
                    <CountryItem country={props.user?.userPerson?.country}/>
                </span>
                <p>{props.user?.userPerson?.country}</p>
            </div>
        </div>
    </div>
}

export default ProfileHeader;