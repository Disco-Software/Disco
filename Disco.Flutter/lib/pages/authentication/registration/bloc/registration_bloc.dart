import 'package:dio/dio.dart';
import 'package:disco_app/data/local/local_storage.dart';
import 'package:disco_app/data/network/repositories/user_repository.dart';
import 'package:disco_app/data/network/request_models/register_request.dart';
import 'package:disco_app/pages/authentication/registration/bloc/registration_event.dart';
import 'package:disco_app/pages/authentication/registration/bloc/registration_state.dart';
import 'package:disco_app/res/strings.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:injectable/injectable.dart';

@injectable
class RegistrationBloc extends Bloc<RegistrationPageEvent, RegistrationPageState> {
  UserRepository userRepository;
  SecureStorageRepository secureStorageRepository;
  Dio dio;

  RegistrationBloc({
    required this.userRepository,
    required this.secureStorageRepository,
    required this.dio,
  }) : super(InitialState()) {
    on<RegistrationEvent>((event, emit) =>
        emit.forEach<RegistrationPageState>(_handleRegistration(event), onData: (state) => state));
  }

  Stream<RegistrationPageState> _handleRegistration(RegistrationEvent event) async* {
    yield RegistratingState();
    final response = await userRepository.registration(RegisterRequestModel(
        userName: event.userName,
        email: event.email,
        password: event.password,
        confirmPassword: event.confirmPassword));
    if (response?.user != null && response?.verificationResult != null) {
      yield RegistratedState();
      secureStorageRepository.write(key: Strings.token, value: response?.verificationResult ?? '');
      secureStorageRepository.write(
          key: Strings.userPhoto, value: response?.user?.profile?.photo ?? '');
      secureStorageRepository.write(key: Strings.userId, value: '${response?.user?.id}');
      dio.options.headers.addAll({'Authorization': 'Bearer: ${response?.verificationResult}'});
    } else {
      final errorText = response?.verificationResult;
      switch (errorText) {
        case "User name is requared":
          yield RegistrationErrorState(userName: "User name is requared");
          break;
        case "Email is required":
          yield RegistrationErrorState(email: errorText);
          break;
        case "Email must be a valid email address":
          RegistrationErrorState(email: errorText);
          break;
        case "Password is required":
          RegistrationErrorState(password: errorText);
          break;
        default:
          "";
          RegistrationErrorState(password: errorText);
          break;
      }
    }
  }
}
