// GENERATED CODE - DO NOT MODIFY BY HAND

// **************************************************************************
// InjectableConfigGenerator
// **************************************************************************

import 'package:dio/dio.dart' as _i6;
import 'package:flutter_secure_storage/flutter_secure_storage.dart' as _i4;
import 'package:get_it/get_it.dart' as _i1;
import 'package:injectable/injectable.dart' as _i2;
import 'package:just_audio/just_audio.dart' as _i3;

import 'data/local/local_storage.dart' as _i5;
import 'data/network/api/auth_api.dart' as _i14;
import 'data/network/api/post_api.dart' as _i7;
import 'data/network/api/search_api.dart' as _i10;
import 'data/network/api/stories_api.dart' as _i12;
import 'data/network/network_client.dart' as _i16;
import 'data/network/repositories/post_repository.dart' as _i8;
import 'data/network/repositories/search_repository.dart' as _i11;
import 'data/network/repositories/stories_repository.dart' as _i13;
import 'data/network/repositories/user_repository.dart' as _i18;
import 'presentation/pages/authentication/login/bloc/login_bloc.dart' as _i19;
import 'presentation/pages/authentication/registration/bloc/registration_bloc.dart'
    as _i21;
import 'presentation/pages/search/bloc/search_page_cubit.dart' as _i17;
import 'presentation/pages/user/main/bloc/like_cubit.dart' as _i15;
import 'presentation/pages/user/main/bloc/posts_cubit.dart' as _i9;
import 'presentation/pages/user/profile/bloc/profile_cubit.dart' as _i20;
import 'register_module.dart' as _i22; // ignore_for_file: unnecessary_lambdas

// ignore_for_file: lines_longer_than_80_chars
/// initializes the registration of provided dependencies inside of [GetIt]
_i1.GetIt $initGetIt(_i1.GetIt get,
    {String? environment, _i2.EnvironmentFilter? environmentFilter}) {
  final gh = _i2.GetItHelper(get, environment, environmentFilter);
  final registerModule = _$RegisterModule();
  gh.lazySingleton<_i3.AudioPlayer>(() => registerModule.audioPlayer());
  gh.lazySingleton<_i4.FlutterSecureStorage>(
      () => registerModule.flutterSecureStorage());
  gh.factory<_i5.SecureStorageRepository>(() => _i5.SecureStorageRepository(
      secureStorage: get<_i4.FlutterSecureStorage>()));
  gh.factory<String>(() => registerModule.baseUrl, instanceName: 'BaseUrl');
  gh.lazySingleton<_i6.Dio>(
      () => registerModule.dio(get<String>(instanceName: 'BaseUrl')));
  gh.factory<_i7.PostApi>(() => _i7.PostApi(dio: get<_i6.Dio>()));
  gh.factory<_i8.PostRepository>(
      () => _i8.PostRepository(postApi: get<_i7.PostApi>()));
  gh.factory<_i9.PostsCubit>(
      () => _i9.PostsCubit(postRepository: get<_i8.PostRepository>()));
  gh.factory<_i10.SearchApi>(() => _i10.SearchApi(dio: get<_i6.Dio>()));
  gh.factory<_i11.SearchRepository>(
      () => _i11.SearchRepository(searchApi: get<_i10.SearchApi>()));
  gh.factory<_i12.StoriesApi>(() => _i12.StoriesApi(dio: get<_i6.Dio>()));
  gh.factory<_i13.StoriesRepository>(
      () => _i13.StoriesRepository(storiesApi: get<_i12.StoriesApi>()));
  gh.factory<_i14.AuthApi>(() => _i14.AuthApi(client: get<_i6.Dio>()));
  gh.factory<_i15.LikeCubit>(
      () => _i15.LikeCubit(postRepository: get<_i8.PostRepository>()));
  gh.factory<_i16.NetworkClient>(() => _i16.NetworkClient(
      dio: get<_i6.Dio>(),
      authApi: get<_i14.AuthApi>(),
      storage: get<_i4.FlutterSecureStorage>()));
  gh.factory<_i17.SearchItemCubit>(() =>
      _i17.SearchItemCubit(searchRepository: get<_i11.SearchRepository>()));
  gh.factory<_i18.UserRepository>(
      () => _i18.UserRepository(authApi: get<_i14.AuthApi>()));
  gh.factory<_i19.LoginBloc>(() => _i19.LoginBloc(
      userRepository: get<_i18.UserRepository>(),
      secureStorageRepository: get<_i5.SecureStorageRepository>(),
      dio: get<_i6.Dio>()));
  gh.factory<_i20.ProfileCubit>(
      () => _i20.ProfileCubit(userRepository: get<_i18.UserRepository>()));
  gh.factory<_i21.RegistrationBloc>(() => _i21.RegistrationBloc(
      userRepository: get<_i18.UserRepository>(),
      secureStorageRepository: get<_i5.SecureStorageRepository>(),
      dio: get<_i6.Dio>()));
  return get;
}

class _$RegisterModule extends _i22.RegisterModule {}
