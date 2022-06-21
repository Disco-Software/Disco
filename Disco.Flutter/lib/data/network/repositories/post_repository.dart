import 'package:dio/dio.dart';
import 'package:disco_app/data/network/api/post_api.dart';
import 'package:disco_app/data/network/network_models/create_post_model.dart';
import 'package:disco_app/data/network/network_models/post_network.dart';
import 'package:injectable/injectable.dart';

@injectable
class PostRepository {
  final PostApi postApi;

  PostRepository({required this.postApi});

  Future<List<Post>?> getAllPosts(int userId) async {
    return postApi.getAllPosts(userId).then((posts) {
      return posts?.map((e) => Post.fromJson(e)).toList();
    });
  }

  Future<List<Post>?> getAllUserPosts(int userId) async {
    return postApi
        .getAllUserPost(userId)
        .then((posts) => posts?.map((e) => Post.fromJson(e)).toList());
  }

  Future<List<Post>?> createPost(CreatePostModel post) async {
    final formData = FormData.fromMap({
      'Description': post.description,
    });

    for (var name in post.postSongNames) {
      formData.fields.addAll([
        MapEntry("PostSongNames", name),
      ]);
    }
    for (var name in post.executorNames) {
      formData.fields.addAll([
        MapEntry("ExecutorNames", name),
      ]);
    }
    for (var file in post.postVideos) {
      formData.files.addAll([
        MapEntry("postVideos", await MultipartFile.fromFile(file)),
      ]);
    }
    for (var file in post.postImages) {
      formData.files.addAll([
        MapEntry("PostImages", await MultipartFile.fromFile(file)),
      ]);
    }
    for (var file in post.postSongImages) {
      formData.files.addAll([
        MapEntry("PostSongImages", await MultipartFile.fromFile(file)),
      ]);
    }
    for (var file in post.postSongs) {
      formData.files.addAll([
        MapEntry("PostSongs", await MultipartFile.fromFile(file)),
      ]);
    }

    return postApi.createPost(formData).then((result) {});
  }
}
