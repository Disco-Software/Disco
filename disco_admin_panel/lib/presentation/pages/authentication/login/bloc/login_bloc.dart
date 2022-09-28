import 'dart:async';

import 'package:dio/dio.dart';
import 'package:disco_admin_panel/core/secure_storage_extensions.dart';
import 'package:disco_admin_panel/data/local/secure_storage.dart';
import 'package:disco_admin_panel/data/network/repositories/user_repository.dart';
import 'package:disco_admin_panel/data/network/request_models/login_request.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:injectable/injectable.dart';

import 'login_event.dart';
import 'login_state.dart';

@injectable
class LoginBloc extends Bloc<LoginPageEvent, LoginState> {
  UserRepository userRepository;
  SecureStorageRepository secureStorageRepository;
  Dio dio;

  LoginBloc({
    required this.userRepository,
    required this.secureStorageRepository,
    required this.dio,
  }) : super(InitLoginState()) {
    on<LoginEvent>(
        (event, emit) => emit.forEach<LoginState>(_handleLogin(event), onData: (state) => state));
  }

  Stream<LoginState> _handleLogin(LoginEvent event) async* {
    try {
      yield LoginingState();
      final authResult = await userRepository.login(LogInRequestModel(
        email: event.email,
        password: event.password,
      ));
      if (authResult?.user != null && authResult?.accesToken != null) {
        yield LoggedInState(userTokenResponse: authResult);
        secureStorageRepository.saveUserModel(
          refreshToken: authResult?.refreshToken,
          token: authResult?.accesToken,
          userId: '${authResult?.user?.id}',
          userName: authResult?.user?.userName,
          userPhoto: authResult?.user?.profile?.photo,
        );
        dio.options.headers.addAll({'Authorization': 'Bearer: ${authResult?.accesToken}'});
      } else {
        final errorText = authResult?.accesToken;
        if (errorText?.contains("Password is not valid") ?? false) {
          yield LogInErrorState(passwordError: errorText);
        } else if (errorText?.contains("user not found") ?? false) {
          yield LogInErrorState(emailError: errorText);
        }
      }
    } catch (err) {
      yield LogInErrorState(emailError: 'Invalid email or password!');
    }
  }
}
